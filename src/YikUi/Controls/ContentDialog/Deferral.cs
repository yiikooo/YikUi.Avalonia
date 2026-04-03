namespace YikUi.Controls;

/// <summary>
/// 用于异步延迟操作完成的回调包装
/// </summary>
public sealed class Deferral
{
    private readonly Action _handler;

    internal Deferral(Action handler)
    {
        _handler = handler ?? throw new ArgumentNullException(nameof(handler));
    }

    /// <summary>
    /// 通知延迟操作已完成
    /// </summary>
    public void Complete() => _handler.Invoke();
}