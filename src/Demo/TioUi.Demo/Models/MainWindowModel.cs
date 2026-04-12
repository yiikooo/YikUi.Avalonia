using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Styling;
using TioUi.Common.Language;

namespace TioUi.Demo.Models;

public sealed class MainWindowModel : INotifyPropertyChanged
{
    public DemoView DemoView { get; } = new();
    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return;
        field = value;
        OnPropertyChanged(propertyName);
    }

    public void ToggleTheme(string? theme = null)
    {
        if (Application.Current != null && theme != null)
        {
            Application.Current.RequestedThemeVariant =
                Application.Current.ActualThemeVariant == ThemeVariant.Dark
                    ? ThemeVariant.Light
                    : ThemeVariant.Dark;
        }

        if (theme == "a")
            Application.Current!.RequestedThemeVariant = ThemeVariant.Default;
        else if (theme == "l")
            Application.Current!.RequestedThemeVariant = ThemeVariant.Light;
        else if (theme == "d")
            Application.Current!.RequestedThemeVariant = ThemeVariant.Dark;
    }


    public void ToggleLang(string? l = null)
    {
        if (l == null) return;

        if (l == "c")
            LangManager.SetLanguage(Languages.zh_cn);
        else if (l == "e")
            LangManager.SetLanguage(Languages.en_us);
    }
}