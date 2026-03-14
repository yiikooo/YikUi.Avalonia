namespace YikUi.Common;

public enum DesktopType
{
    Windows,
    MacOs,
    Linux,
    FreeBSD,
    Unknown,
    Undefine
}

public enum DialogMode
{
    Info,
    Warning,
    Error,
    Question,
    None,
    Success
}

public enum DialogButton
{
    None,
    OK,
    OKCancel,
    YesNo,
    YesNoCancel
}

public enum Position
{
    Left,
    Top,
    Right,
    Bottom
}

public enum SearchBoxPlacement
{
    Top,
    Bottom
}

public enum CornerPosition
{
    TopLeft,
    TopRight,
    BottomLeft,
    BottomRight,
}

public enum ItemAlignment
{
    Center,
    Justify,
    Left,
    Plain,
}

public enum DialogResult
{
    Cancel,
    No,
    None,
    OK,
    Yes,
}