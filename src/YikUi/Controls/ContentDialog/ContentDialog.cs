using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Presenters;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Threading;
using Avalonia.VisualTree;

namespace YikUi.Controls;

/// <summary>
/// 异步内容对话框，提供标题、内容以及主要/次要/关闭三种按钮的模态对话框
/// </summary>
public partial class ContentDialog : ContentControl, ICustomKeyboardNavigation
{
    private Button? _closeButton;
    private bool _hasDeferralActive;
    private ContentDialogHost? _host;
    private Visual? _hotkeyDownVisual;

    private IInputElement? _lastFocus;
    private Control? _originalHost;
    private int _originalHostIndex;
    private Button? _primaryButton;
    private ContentDialogResult _result;
    private Button? _secondaryButton;
    private TaskCompletionSource<ContentDialogResult> _tcs = null!;

    public ContentDialog()
    {
        PseudoClasses.Add(PC_Hidden);
    }

    // ICustomKeyboardNavigation — 将 Tab/Shift+Tab 的焦点限制在对话框内循环
    public (bool handled, IInputElement? next) GetNext(IInputElement element, NavigationDirection direction)
    {
        var children = this.GetVisualDescendants()
            .OfType<IInputElement>()
            .Where(x => x is InputElement ie &&
                        KeyboardNavigation.GetIsTabStop(ie) &&
                        x.Focusable &&
                        x.IsEffectivelyVisible &&
                        ie.IsEffectivelyEnabled)
            .ToList();

        if (children.Count == 0) return (false, null);

        var focused = TopLevel.GetTopLevel(this)?.FocusManager?.GetFocusedElement();
        if (focused == null) return (false, null);

        if (direction == NavigationDirection.Next)
        {
            for (int i = 0; i < children.Count; i++)
            {
                if (children[i] == focused)
                    return (true, i == children.Count - 1 ? children[0] : children[i + 1]);
            }
        }
        else if (direction == NavigationDirection.Previous)
        {
            for (int i = children.Count - 1; i >= 0; i--)
            {
                if (children[i] == focused)
                    return (true, i == 0 ? children[^1] : children[i - 1]);
            }
        }

        return (false, null);
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        if (_primaryButton != null) _primaryButton.Click -= OnButtonClick;
        if (_secondaryButton != null) _secondaryButton.Click -= OnButtonClick;
        if (_closeButton != null) _closeButton.Click -= OnButtonClick;

        base.OnApplyTemplate(e);

        _primaryButton = e.NameScope.Get<Button>(PART_PrimaryButton);
        _primaryButton.Click += OnButtonClick;
        _secondaryButton = e.NameScope.Get<Button>(PART_SecondaryButton);
        _secondaryButton.Click += OnButtonClick;
        _closeButton = e.NameScope.Get<Button>(PART_CloseButton);
        _closeButton.Click += OnButtonClick;
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);

        if (change.Property == FullSizeDesiredProperty)
            PseudoClasses.Set(PC_FullSize, change.GetNewValue<bool>());
    }

    protected override bool RegisterContentPresenter(ContentPresenter presenter)
    {
        if (presenter.Name == "PART_ContentPresenter")
            return true;
        return base.RegisterContentPresenter(presenter);
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
        base.OnKeyDown(e);
        if (!e.Handled && (e.Key == Key.Enter || e.Key == Key.Escape))
            _hotkeyDownVisual = e.Source as Visual;
    }

    protected override void OnKeyUp(KeyEventArgs e)
    {
        if (e.Handled)
        {
            base.OnKeyUp(e);
            return;
        }

        // 防止对话框因打开时的 Enter/Escape 抬键事件误触发关闭
        if ((e.Key == Key.Enter || e.Key == Key.Escape) &&
            (_hotkeyDownVisual == null || _hotkeyDownVisual != e.Source as Visual))
        {
            base.OnKeyUp(e);
            return;
        }

        switch (e.Key)
        {
            case Key.Escape:
                HideCore();
                e.Handled = true;
                break;

            case Key.Enter:
                if (DefaultButton != ContentDialogButton.None)
                {
                    switch (DefaultButton)
                    {
                        case ContentDialogButton.Primary:
                            OnButtonClick(_primaryButton!, null);
                            break;
                        case ContentDialogButton.Secondary:
                            OnButtonClick(_secondaryButton!, null);
                            break;
                        case ContentDialogButton.Close:
                            OnButtonClick(_closeButton!, null);
                            break;
                    }

                    e.Handled = true;
                }

                break;
        }

        base.OnKeyUp(e);
    }

    /// <summary>
    /// 异步显示对话框。自动查找当前活动窗口。
    /// </summary>
    public Task<ContentDialogResult> ShowAsync() => ShowAsyncCore(null);

    /// <summary>
    /// 在指定窗口上异步显示对话框
    /// </summary>
    public Task<ContentDialogResult> ShowAsync(Window window) => ShowAsyncCore(window);

    /// <summary>
    /// 在指定 TopLevel 上异步显示对话框（适用于无 ApplicationLifetime 场景，如单元测试）
    /// </summary>
    public Task<ContentDialogResult> ShowAsync(TopLevel topLevel) => ShowAsyncCore(topLevel);

    private async Task<ContentDialogResult> ShowAsyncCore(TopLevel? topLevel)
    {
        _tcs = new TaskCompletionSource<ContentDialogResult>();

        OnOpening();

        // 若对话框已在某个父容器中，先从父容器移除并记录位置
        if (Parent != null)
        {
            _originalHost = (Control)Parent;
            switch (_originalHost)
            {
                case Panel p:
                    _originalHostIndex = p.Children.IndexOf(this);
                    p.Children.Remove(this);
                    break;
                case Decorator d:
                    d.Child = null;
                    break;
                case ContentControl cc:
                    cc.Content = null;
                    break;
                case ContentPresenter cp:
                    cp.Content = null;
                    break;
            }
        }

        _host ??= new ContentDialogHost();
        _host.Content = this;

        // 查找 OverlayLayer
        OverlayLayer? overlayLayer = null;

        if (topLevel != null)
        {
            overlayLayer = ResolveOverlayLayer(topLevel);
        }
        else
        {
            if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                var windows = desktop.Windows;
                for (int i = 0; i < windows.Count; i++)
                {
                    if (windows[i].IsActive)
                    {
                        topLevel = windows[i];
                        break;
                    }
                }

                topLevel ??= desktop.MainWindow
                             ?? throw new InvalidOperationException("未找到可用的 TopLevel 来承载 ContentDialog");

                overlayLayer = ResolveOverlayLayer(topLevel);
            }
            else if (Application.Current?.ApplicationLifetime is ISingleViewApplicationLifetime singleView)
            {
                topLevel = TopLevel.GetTopLevel(singleView.MainView);
                if (topLevel != null)
                    overlayLayer = ResolveOverlayLayer(topLevel);
            }
            else
            {
                throw new InvalidOperationException(
                    "未找到 TopLevel，请向 ShowAsync() 传入 Window 或 TopLevel，或确保 ApplicationLifetime 已正确配置");
            }
        }

        if (overlayLayer == null)
            throw new InvalidOperationException("无法从指定的 TopLevel 获取 OverlayLayer。请确保窗口模板中包含 VisualLayerManager。");

        _lastFocus = topLevel?.FocusManager?.GetFocusedElement();

        overlayLayer.Children.Add(_host);

        IsVisible = true;
        PseudoClasses.Set(PC_Hidden, false);
        PseudoClasses.Set(PC_Open, true);

        // 等待模板加载完成后再初始化
        Loaded += OnDialogLoaded;

        return await _tcs.Task;
    }

    /// <summary>
    /// 查找 TopLevel 下的 OverlayLayer。
    /// 标准做法是从 visual 向上找 VisualLayerManager；
    /// 但对于 Window 本身（无祖先），需向下搜索其 visual 树。
    /// </summary>
    private static OverlayLayer? ResolveOverlayLayer(Visual visual)
    {
        // 标准路径：从 visual 向上（含自身）找 VisualLayerManager
        var ol = OverlayLayer.GetOverlayLayer(visual);
        if (ol != null) return ol;

        // 回退路径：Window 是根节点，GetOverlayLayer 无法在祖先中找到 VisualLayerManager，
        // 改为向下遍历 visual 子树，直接找 OverlayLayer 控件实例。
        // 适用于 YikWindow 等将 <VisualLayerManager> 放在模板内的自定义窗口。
        return visual.GetVisualDescendants().OfType<OverlayLayer>().FirstOrDefault();
    }

    /// <summary>
    /// 关闭对话框，结果为 <see cref="ContentDialogResult.None"/>
    /// </summary>
    public void Hide() => Hide(ContentDialogResult.None);

    /// <summary>
    /// 以指定结果关闭对话框
    /// </summary>
    public void Hide(ContentDialogResult result)
    {
        _result = result;
        HideCore();
    }

    protected virtual void OnPrimaryButtonClick(ContentDialogButtonClickEventArgs args) =>
        PrimaryButtonClick?.Invoke(this, args);

    protected virtual void OnSecondaryButtonClick(ContentDialogButtonClickEventArgs args) =>
        SecondaryButtonClick?.Invoke(this, args);

    protected virtual void OnCloseButtonClick(ContentDialogButtonClickEventArgs args) =>
        CloseButtonClick?.Invoke(this, args);

    protected virtual void OnOpening() => Opening?.Invoke(this, EventArgs.Empty);

    protected virtual void OnOpened() => Opened?.Invoke(this, EventArgs.Empty);

    protected virtual void OnClosing(ContentDialogClosingEventArgs args) => Closing?.Invoke(this, args);

    protected virtual void OnClosed(ContentDialogClosedEventArgs args) => Closed?.Invoke(this, args);

    private void HideCore()
    {
        if (_hasDeferralActive) return;

        var args = new ContentDialogClosingEventArgs(_result);

        var deferral = new Deferral(() =>
        {
            Dispatcher.UIThread.VerifyAccess();
            _hasDeferralActive = false;

            if (!args.Cancel)
                FinalCloseDialog();
        });

        args.SetDeferral(deferral);
        _hasDeferralActive = true;

        args.IncrementDeferralCount();
        OnClosing(args);
        args.DecrementDeferralCount();
    }

    private async void FinalCloseDialog()
    {
        IsHitTestVisible = false;

        // 先聚焦自身以隐藏焦点框，避免关闭动画时焦点框残留
        Focus();

        PseudoClasses.Set(PC_Hidden, true);
        PseudoClasses.Set(PC_Open, false);

        // 等待关闭动画完成
        await Task.Delay(200);

        OnClosed(new ContentDialogClosedEventArgs(_result));

        if (_lastFocus != null)
        {
            _lastFocus.Focus(NavigationMethod.Unspecified);
            _lastFocus = null;
        }

        var overlayLayer = OverlayLayer.GetOverlayLayer(_host!);
        if (overlayLayer == null) return; // 防止重入调用

        overlayLayer.Children.Remove(_host!);
        _host!.Content = null;

        // 将对话框还原到原来的父容器
        if (_originalHost != null)
        {
            switch (_originalHost)
            {
                case Panel p:
                    p.Children.Insert(_originalHostIndex, this);
                    break;
                case Decorator d:
                    d.Child = this;
                    break;
                case ContentControl cc:
                    cc.Content = this;
                    break;
                case ContentPresenter cp:
                    cp.Content = this;
                    break;
            }

            _originalHost = null;
        }

        IsHitTestVisible = true;
        _hotkeyDownVisual = null;
        _tcs.TrySetResult(_result);
    }

    private void OnButtonClick(object? sender, RoutedEventArgs? e)
    {
        if (_hasDeferralActive) return;

        var args = new ContentDialogButtonClickEventArgs();

        var deferral = new Deferral(() =>
        {
            Dispatcher.UIThread.VerifyAccess();
            _hasDeferralActive = false;

            if (args.Cancel) return;

            if (sender == _primaryButton)
            {
                if (PrimaryButtonCommand?.CanExecute(PrimaryButtonCommandParameter) == true)
                    PrimaryButtonCommand.Execute(PrimaryButtonCommandParameter);
                _result = ContentDialogResult.Primary;
            }
            else if (sender == _secondaryButton)
            {
                if (SecondaryButtonCommand?.CanExecute(SecondaryButtonCommandParameter) == true)
                    SecondaryButtonCommand.Execute(SecondaryButtonCommandParameter);
                _result = ContentDialogResult.Secondary;
            }
            else if (sender == _closeButton)
            {
                if (CloseButtonCommand?.CanExecute(CloseButtonCommandParameter) == true)
                    CloseButtonCommand.Execute(CloseButtonCommandParameter);
                _result = ContentDialogResult.None;
            }

            HideCore();
        });

        args.SetDeferral(deferral);
        _hasDeferralActive = true;

        args.IncrementDeferralCount();
        if (sender == _primaryButton)
            OnPrimaryButtonClick(args);
        else if (sender == _secondaryButton)
            OnSecondaryButtonClick(args);
        else if (sender == _closeButton)
            OnCloseButtonClick(args);
        args.DecrementDeferralCount();
    }

    private void SetupDialog()
    {
        if (_primaryButton == null)
            throw new InvalidOperationException("对话框模板尚未应用，无法执行 SetupDialog");

        PseudoClasses.Set(PC_Primary, !string.IsNullOrEmpty(PrimaryButtonText));
        PseudoClasses.Set(PC_Secondary, !string.IsNullOrEmpty(SecondaryButtonText));
        PseudoClasses.Set(PC_Close, !string.IsNullOrEmpty(CloseButtonText));

        // 移除所有按钮的 accent class
        _primaryButton.Classes.Remove("accent");
        _secondaryButton.Classes.Remove("accent");
        _closeButton.Classes.Remove("accent");

        switch (DefaultButton)
        {
            case ContentDialogButton.Primary when _primaryButton.IsVisible:
                _primaryButton.Classes.Add("accent");
                _primaryButton.Focus();
                break;

            case ContentDialogButton.Secondary when _secondaryButton.IsVisible:
                _secondaryButton.Classes.Add("accent");
                _secondaryButton.Focus();
                break;

            case ContentDialogButton.Close when _closeButton.IsVisible:
                _closeButton.Classes.Add("accent");
                _closeButton.Focus();
                break;

            default:
                var next = this.GetVisualDescendants()
                               .OfType<InputElement>()
                               .FirstOrDefault(x =>
                                   KeyboardNavigation.GetIsTabStop(x) && x.Focusable && x.IsEffectivelyVisible &&
                                   x.IsEffectivelyEnabled)
                           ?? (IInputElement)this;
                next.Focus();
                break;
        }
    }

    private void OnDialogLoaded(object? sender, RoutedEventArgs args)
    {
        Loaded -= OnDialogLoaded;
        SetupDialog();
        UpdateLayout();
        OnOpened();
    }
}