namespace YikUi.Controls;

/// <summary>
/// 为对话框关闭后事件提供数据
/// </summary>
public class ContentDialogClosedEventArgs : EventArgs
{
    internal ContentDialogClosedEventArgs(ContentDialogResult result)
    {
        Result = result;
    }

    /// <summary>
    /// 获取对话框关闭时的 <see cref="ContentDialogResult"/>
    /// </summary>
    public ContentDialogResult Result { get; }
}