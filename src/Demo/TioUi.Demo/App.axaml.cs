using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;
using TioUi.Common.Language;
using TioUi.Demo.Models;

namespace TioUi.Demo;

public class App : Application
{
    public static IView RootView { get; private set; }

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var desktopMainWindow = new MainWindow();
            RootView = desktopMainWindow;
            desktop.MainWindow = desktopMainWindow;
        }
        else
        {
            var singleViewMainView = new MainView();
            RootView = singleViewMainView;
            if (ApplicationLifetime is IActivityApplicationLifetime activityLifetime)
            {
                activityLifetime.MainViewFactory = () => singleViewMainView;
            }
            else if (ApplicationLifetime is ISingleViewApplicationLifetime singleView)
            {
                singleView.MainView = singleViewMainView;
            }
        }

        this.AttachDeveloperTools();
        base.OnFrameworkInitializationCompleted();
    }

    private void About_OnClick(object? sender, EventArgs e)
    {
        // 可在此弹出关于对话框
    }

    private void ThemeDefault_OnClick(object? sender, EventArgs e) =>
        RequestedThemeVariant = ThemeVariant.Default;

    private void ThemeLight_OnClick(object? sender, EventArgs e) =>
        RequestedThemeVariant = ThemeVariant.Light;

    private void ThemeDark_OnClick(object? sender, EventArgs e) =>
        RequestedThemeVariant = ThemeVariant.Dark;

    private void LangChinese_OnClick(object? sender, EventArgs e) =>
        LangManager.SetLanguage(Languages.zh_cn);

    private void LangEnglish_OnClick(object? sender, EventArgs e) =>
        LangManager.SetLanguage(Languages.en_us);
}