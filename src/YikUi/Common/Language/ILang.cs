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
}