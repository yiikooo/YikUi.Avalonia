using Avalonia.Threading;

namespace TioUi.Controls;

/// <summary>
/// 为按钮点击事件提供数据，支持异步延迟
/// </summary>
public class ContentDialogButtonClickEventArgs : EventArgs
{
    private Deferral? _deferral;
    private int _deferralCount;

    internal ContentDialogButtonClickEventArgs()
    {
    }

    /// <summary>
    /// 获取或设置是否取消按钮的默认行为（取消时对话框不会关闭）
    /// </summary>
    public bool Cancel { get; set; }

    /// <summary>
    /// 获取一个 <see cref="Deferral"/>，用于在按钮点击后执行异步操作
    /// </summary>
    public Deferral GetDeferral()
    {
        _deferralCount++;
        return new Deferral(() =>
        {
            Dispatcher.UIThread.VerifyAccess();
            DecrementDeferralCount();
        });
    }

    internal void SetDeferral(Deferral deferral) => _deferral = deferral;

    internal void IncrementDeferralCount() => _deferralCount++;

    internal void DecrementDeferralCount()
    {
        _deferralCount--;
        if (_deferralCount == 0)
            _deferral?.Complete();
    }
}