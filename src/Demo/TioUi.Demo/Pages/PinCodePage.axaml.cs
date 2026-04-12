using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TioUi.Controls;

namespace TioUi.Demo.Pages;

public partial class PinCodePage : UserControl
{
    public PinCodePage()
    {
        InitializeComponent();
        DataContext = new PinCodeDemoViewModel();
    }

    private async void VerificationCode_OnComplete(object? _, PinCodeCompleteEventArgs e)
    {
        var text = string.Join(string.Empty, e.Code);
        App.RootView.ToastManager.Show(text);
    }
}

public partial class PinCodeDemoViewModel : ObservableObject
{
    [ObservableProperty] private List<Exception>? _error;

    public PinCodeDemoViewModel()
    {
        CompleteCommand = new AsyncRelayCommand<IList<string>>(OnComplete);
        Error = [new Exception("Invalid verification code")];
    }

    public ICommand CompleteCommand { get; set; }

    private async Task OnComplete(IList<string>? obj)
    {
        if (obj is null) return;
        var code = string.Join("", obj);
        App.RootView.ToastManager.Show(code);
    }
}