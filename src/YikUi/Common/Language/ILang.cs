namespace YikUi.Common.Language;

public interface ILang
{
    public string Name { get; }
    public string FileName { get; }
    public string UpdateAt { get; }
    public string Type { get; }
    public string Size { get; }
    public string Cancel { get; }
    public string Confirm { get; }
    public string ShowHiddenFiles { get; }
    public string FileAlreadyExists { get; }
    public string Day { get; }
    public string Month { get; }
    public string Year { get; }
    public string Hour { get; }
    public string Minute { get; }
    public string Second { get; }
    public string Undo { get; }
    public string Redo { get; }
    public string SelectAll { get; }
    public string Cut { get; }
    public string Copy { get; }
    public string Paste { get; }
    public string Clear { get; }
}