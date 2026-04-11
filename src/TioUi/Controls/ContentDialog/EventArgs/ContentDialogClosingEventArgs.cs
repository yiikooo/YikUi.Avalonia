using Avalonia.Threading;

namespace TioUi.Controls;

/// <summary>
/// 为对话框关闭前事件提供数据，支持取消和异步延迟
/// </summary>
public class ContentDialogClosingEventArgs : EventArgs
{
    private Deferral? _deferral;
    private int _deferralCount;

    internal ContentDialogClosingEventArgs(ContentDialogResult result)
    {
        Result = result;
    }

    /// <summary>
    /// 获取关闭事件的 <see cref="ContentDialogResult"/>
    /// </summary>
    public ContentDialogResult Result { get; }

    /// <summary>
    /// 获取或设置是否取消关闭操作
    /// </summary>
    public bool Cancel { get; set; }

    /// <summary>
    /// 获取一个 <see cref="Deferral"/>，用于在关闭前执行异步操作
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