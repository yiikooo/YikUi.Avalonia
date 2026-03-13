using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Media;
using Avalonia.Styling;
using YikUi.Common.Language;

namespace YikUi.Demo;

public class Setting : INotifyPropertyChanged
{
    private static Setting? _instance;

    public Setting()
    {
        PropertyChanged += OnPropertyChanged;
        Application.Current!.ActualThemeVariantChanged += (_, _) =>
        {
            Theme = Application.Current.RequestedThemeVariant ?? ThemeVariant.Default;
        };
    }

    public static Setting Instance => _instance ??= new Setting();

    public ThemeVariant Theme
    {
        get;
        set => SetField(ref field, value);
    } = ThemeVariant.Default;

    public Color ThemeColor
    {
        get;
        set => SetField(ref field, value);
    } = Color.Parse("#1890ff");

    public Languages Language
    {
        get;
        set => SetField(ref field, value);
    } = Languages.zh_cn;

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(Theme))
            Application.Current!.RequestedThemeVariant = Theme;
        else if (e.PropertyName == nameof(ThemeColor))
        {
            var yikTheme = Application.Current?.Styles
                .OfType<YikUiTheme>()
                .FirstOrDefault();
            yikTheme?.SetThemeColor(ThemeColor);
        }
        else if (e.PropertyName == nameof(Language))
            LangManager.SetLanguage(Language);
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}