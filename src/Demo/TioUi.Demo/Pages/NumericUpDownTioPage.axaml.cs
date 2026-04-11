using System;
using System.Globalization;
using Avalonia.Controls;
using Avalonia.Layout;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace TioUi.Demo.Pages;

public partial class NumericUpDownTioPage : UserControl
{
    public NumericUpDownTioPage()
    {
        InitializeComponent();
        DataContext = new NumericUpDownDemoViewModel();
    }
}

public partial class NumericUpDownDemoViewModel : ObservableObject
{
    [ObservableProperty] private bool _allowDrag;
    [ObservableProperty] private bool _allowSpin = true;

    [ObservableProperty] private Array _arrayHorizontalAlignment;

    [ObservableProperty] private Array _arrayHorizontalContentAlignment;
    [ObservableProperty] private Array _arrayParsingNumberStyle;
    [ObservableProperty] private bool _autoWidth = true;

    [ObservableProperty] private string _commandUpdateText = "Command not Execute";
    [ObservableProperty] private string _fontFamily = "Consolas";
    [ObservableProperty] private string _formatString = "X8";
    [ObservableProperty] private HorizontalAlignment _horizontalAlignment = HorizontalAlignment.Center;
    [ObservableProperty] private HorizontalAlignment _horizontalContentAlignment = HorizontalAlignment.Center;
    [ObservableProperty] private object? _innerLeftContent = "obj:0x";
    [ObservableProperty] private object? _innerRightContent = "%";

    [ObservableProperty] private bool _isEnable = true;
    [ObservableProperty] private bool _isReadOnly;

    [ObservableProperty] private UInt32 _maximum = UInt32.MaxValue;
    [ObservableProperty] private UInt32 _minimum = UInt32.MinValue;


    private double _oldWidth = 300;
    [ObservableProperty] private NumberStyles _parsingNumberStyle = NumberStyles.AllowHexSpecifier;
    [ObservableProperty] private string _PlaceholderText = "Water mark showed";
    [ObservableProperty] private bool _showButtonSpinner = true;
    [ObservableProperty] private UInt32 _step = 1;
    [ObservableProperty] private uint _value;
    [ObservableProperty] private double _width = double.NaN;


    public NumericUpDownDemoViewModel()
    {
        ArrayHorizontalContentAlignment = Enum.GetValues(typeof(HorizontalAlignment));
        ArrayHorizontalAlignment = Enum.GetValues(typeof(HorizontalAlignment));
        ArrayParsingNumberStyle = Enum.GetValues(typeof(NumberStyles));
    }

    [RelayCommand]
    void Trythis(uint v)
    {
        CommandUpdateText = $"Command Exe,CommandParameter={v}";
    }

    partial void OnAutoWidthChanged(bool value)
    {
        if (value)
        {
            _oldWidth = Width;
            Width = double.NaN;
        }
        else
        {
            Width = _oldWidth;
        }
    }
}