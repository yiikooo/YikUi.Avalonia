namespace YikUi.Controls;

/// <summary>
/// 指定 ContentDialog 的返回结果
/// </summary>
public enum ContentDialogResult
{
    /// <summary>
    /// 未点击任何按钮
    /// </summary>
    None = 0,

    /// <summary>
    /// 点击了主要按钮
    /// </summary>
    Primary = 1,

    /// <summary>
    /// 点击了次要按钮
    /// </summary>
    Secondary = 2
}