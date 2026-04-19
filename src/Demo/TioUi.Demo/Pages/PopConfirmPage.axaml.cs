using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Notifications;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TioUi.Controls;

namespace TioUi.Demo.Pages;

public partial class PopConfirmPage : UserControl
{
    public PopConfirmPage()
    {
        InitializeComponent();
        DataContext = new PopConfirmDemoViewModel();
    }
}

public class PopConfirmDemoViewModel : ObservableObject
{
    public PopConfirmDemoViewModel()
    {
        AsyncConfirmCommand = new AsyncRelayCommand(OnConfirmAsync);
        AsyncCancelCommand = new RelayCommand(OnCancelAsync);
        ConfirmCommand = new RelayCommand(OnConfirm);
        CancelCommand = new RelayCommand(OnCancel);
    }

    internal TioToastManager? ToastManager => App.RootView.ToastManager;

    public ICommand ConfirmCommand { get; }
    public ICommand CancelCommand { get; }

    public ICommand AsyncConfirmCommand { get; }
    public ICommand AsyncCancelCommand { get; }

    private void OnCancel()
    {
        ToastManager?.Show("Canceled", NotificationType.Error);
    }

    private void OnConfirm()
    {
        ToastManager?.Show("Confirmed", NotificationType.Success);
    }

    private async Task OnConfirmAsync()
    {
        await Task.Delay(3000);
        ToastManager?.Show("Async Confirmed", NotificationType.Success);
    }

    private void OnCancelAsync()
    {
        ToastManager?.Show("Async Canceled", NotificationType.Error);
    }
}