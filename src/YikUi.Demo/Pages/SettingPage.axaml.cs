namespace YikUi.Demo.Pages;

public partial class SettingPage : PageModelBase
{
    public SettingPage()
    {
        InitializeComponent();
        DataContext = Setting.Instance;
    }
}