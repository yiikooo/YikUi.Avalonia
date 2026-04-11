using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TioUi.Common;
using TioUi.Controls;
using TioUi.Demo.Dialogs;

namespace TioUi.Demo.Pages;

public partial class DialogPage : UserControl
{
    public DialogPage()
    {
        InitializeComponent();
        DataContext = new DialogDemoViewModel();
    }
}

public partial class DialogDemoViewModel : ObservableObject
{
    public const string LocalHost = "LocalHost";
    public DefaultWindowDialogDemoViewModel DefaultWindowDialogDemoViewModel { get; set; } = new();
    public CustomWindowDialogDemoViewModel CustomWindowDialogDemoViewModel { get; set; } = new();
    public DefaultOverlayDialogDemoViewModel DefaultOverlayDialogDemoViewModel { get; set; } = new();
    public CustomOverlayDialogDemoViewModel CustomOverlayDialogDemoViewModel { get; set; } = new();
}

public partial class DefaultWindowDialogDemoViewModel : ObservableObject
{
    [ObservableProperty] private DialogButton _button;
    [ObservableProperty] private bool _canDragMove;
    [ObservableProperty] private bool _canResize;
    [ObservableProperty] private bool? _isCloseButtonVisible;
    [ObservableProperty] private WindowStartupLocation _location;
    [ObservableProperty] private DialogMode _mode;
    [ObservableProperty] private bool _showInTaskBar;
    [ObservableProperty] private string? _styleClass;
    [ObservableProperty] private string? _title;
    [ObservableProperty] private int? _x;
    [ObservableProperty] private int? _y;

    public DefaultWindowDialogDemoViewModel()
    {
        ShowDialogCommand = new AsyncRelayCommand(ShowDialog);
        Mode = DialogMode.None;
        Button = DialogButton.OKCancel;
        Location = WindowStartupLocation.CenterScreen;
        CanDragMove = true;
    }

    public ICommand ShowDialogCommand { get; }

    private async Task ShowDialog()
    {
        if (OperatingSystem.IsBrowser() || OperatingSystem.IsAndroid() || OperatingSystem.IsIOS())
        {
            await MessageBox.ShowOverlayAsync(
                "Window dialogs are not supported on this platform. Please use overlay dialogs instead.");
            return;
        }

        var options = new DialogOptions()
        {
            Title = Title,
            Mode = Mode,
            Button = Button,
            ShowInTaskBar = ShowInTaskBar,
            IsCloseButtonVisible = IsCloseButtonVisible,
            StartupLocation = Location,
            CanDragMove = CanDragMove,
            CanResize = CanResize,
            StyleClass = StyleClass,
        };
        if (X.HasValue && Y.HasValue)
        {
            options.Position = new PixelPoint(X.Value, Y.Value);
        }

        await Dialog.ShowModal<DefaultDemoDialog, DefaultDemoDialogViewModel>(new DefaultDemoDialogViewModel(),
            options: options);
    }
}

public partial class CustomWindowDialogDemoViewModel : ObservableObject
{
    [ObservableProperty] private bool _canDragMove;
    [ObservableProperty] private bool _canResize;
    [ObservableProperty] private bool? _isCloseButtonVisible;
    [ObservableProperty] private bool _isModal;
    [ObservableProperty] private WindowStartupLocation _location;
    [ObservableProperty] private bool _showInTaskBar;
    [ObservableProperty] private string? _title;
    [ObservableProperty] private int? _x;
    [ObservableProperty] private int? _y;

    public CustomWindowDialogDemoViewModel()
    {
        ShowDialogCommand = new AsyncRelayCommand(ShowDialog);
        Location = WindowStartupLocation.CenterScreen;
        IsModal = true;
        CanDragMove = true;
    }

    public ICommand ShowDialogCommand { get; }

    private async Task ShowDialog()
    {
        if (OperatingSystem.IsBrowser() || OperatingSystem.IsAndroid() || OperatingSystem.IsIOS())
        {
            await MessageBox.ShowOverlayAsync(
                "Window dialogs are not supported on this platform. Please use overlay dialogs instead.");
            return;
        }

        var options = new DialogOptions()
        {
            Title = Title,
            ShowInTaskBar = ShowInTaskBar,
            IsCloseButtonVisible = IsCloseButtonVisible,
            StartupLocation = Location,
            CanDragMove = CanDragMove,
            CanResize = CanResize,
        };
        if (X.HasValue && Y.HasValue)
        {
            options.Position = new PixelPoint(X.Value, Y.Value);
        }

        if (IsModal)
        {
            await Dialog.ShowCustomModal<CustomDemoDialog, CustomDemoDialogViewModel, object>(
                new CustomDemoDialogViewModel(),
                options: options);
        }
        else
        {
            Dialog.ShowCustom<CustomDemoDialog, CustomDemoDialogViewModel>(new CustomDemoDialogViewModel(),
                options: options);
        }
    }
}

public partial class DefaultOverlayDialogDemoViewModel : ObservableObject
{
    [ObservableProperty] private DialogButton _button;
    [ObservableProperty] private bool _canDragMove;
    [ObservableProperty] private bool _canLightDismiss;
    [ObservableProperty] private bool _canResize;
    [ObservableProperty] private bool _fullScreen;
    [ObservableProperty] private HorizontalPosition _horizontalAnchor;
    [ObservableProperty] private double? _horizontalOffset;
    [ObservableProperty] private bool? _isCloseButtonVisible;
    [ObservableProperty] private bool _isLocal;
    [ObservableProperty] private bool _isModal;
    [ObservableProperty] private DialogMode _mode;
    [ObservableProperty] private string? _styleClass;
    [ObservableProperty] private string? _title;
    [ObservableProperty] private VerticalPosition _verticalAnchor;
    [ObservableProperty] private double? _verticalOffset;

    public DefaultOverlayDialogDemoViewModel()
    {
        ShowDialogCommand = new AsyncRelayCommand(ShowDialog);
        HorizontalAnchor = HorizontalPosition.Center;
        VerticalAnchor = VerticalPosition.Center;
        CanDragMove = true;
        IsModal = true;
        IsCloseButtonVisible = true;
        Button = DialogButton.OKCancel;
    }

    public ICommand ShowDialogCommand { get; }

    private async Task ShowDialog()
    {
        var options = new OverlayDialogOptions()
        {
            FullScreen = FullScreen,
            HorizontalAnchor = HorizontalAnchor,
            VerticalAnchor = VerticalAnchor,
            HorizontalOffset = HorizontalOffset,
            VerticalOffset = VerticalOffset,
            Mode = Mode,
            Buttons = Button,
            Title = Title,
            CanLightDismiss = CanLightDismiss,
            CanDragMove = CanDragMove,
            IsCloseButtonVisible = IsCloseButtonVisible,
            CanResize = CanResize,
            StyleClass = StyleClass,
        };
        string? dialogHostId = IsLocal ? DialogDemoViewModel.LocalHost : null;
        if (IsModal)
        {
            await OverlayDialog.ShowModal<DefaultDemoDialog, DefaultDemoDialogViewModel>(
                new DefaultDemoDialogViewModel(), dialogHostId, options: options);
        }
        else
        {
            OverlayDialog.Show<DefaultDemoDialog, DefaultDemoDialogViewModel>(new DefaultDemoDialogViewModel(),
                dialogHostId, options: options);
        }
    }
}

public partial class CustomOverlayDialogDemoViewModel : ObservableObject
{
    [ObservableProperty] private bool _canDragMove;
    [ObservableProperty] private bool _canLightDismiss;
    [ObservableProperty] private bool _canResize;
    [ObservableProperty] private bool _fullScreen;
    [ObservableProperty] private HorizontalPosition _horizontalAnchor;
    [ObservableProperty] private double? _horizontalOffset;
    [ObservableProperty] private bool? _isCloseButtonVisible;
    [ObservableProperty] private bool _isLocal;
    [ObservableProperty] private bool _isModal;
    [ObservableProperty] private string? _title;
    [ObservableProperty] private VerticalPosition _verticalAnchor;
    [ObservableProperty] private double? _verticalOffset;

    public CustomOverlayDialogDemoViewModel()
    {
        ShowDialogCommand = new AsyncRelayCommand(ShowDialog);
        HorizontalAnchor = HorizontalPosition.Center;
        VerticalAnchor = VerticalPosition.Center;
        CanDragMove = true;
        IsModal = true;
    }

    public ICommand ShowDialogCommand { get; }

    private async Task ShowDialog()
    {
        var options = new OverlayDialogOptions()
        {
            FullScreen = FullScreen,
            HorizontalAnchor = HorizontalAnchor,
            VerticalAnchor = VerticalAnchor,
            HorizontalOffset = HorizontalOffset,
            VerticalOffset = VerticalOffset,
            Title = Title,
            CanLightDismiss = CanLightDismiss,
            CanDragMove = CanDragMove,
            IsCloseButtonVisible = IsCloseButtonVisible,
            CanResize = CanResize,
        };
        var dialogHostId = IsLocal ? DialogDemoViewModel.LocalHost : null;
        if (IsModal)
        {
            await OverlayDialog.ShowCustomModal<CustomDemoDialog, CustomDemoDialogViewModel, object>(
                new CustomDemoDialogViewModel(), dialogHostId, options: options);
        }
        else
        {
            OverlayDialog.ShowCustom<CustomDemoDialog, CustomDemoDialogViewModel>(new CustomDemoDialogViewModel(),
                dialogHostId, options: options);
        }
    }
}