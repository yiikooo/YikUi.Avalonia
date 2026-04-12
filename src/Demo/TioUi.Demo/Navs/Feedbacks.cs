using System.Collections.Generic;
using TioUi.Demo.Models;
using TioUi.Demo.Pages;

namespace TioUi.Demo.Navs;

public static class Feedbacks
{
    public static readonly List<Page> FeedbacksList =
    [
        new()
        {
            Title = "DataValidationErrors",
            Content = new DataValidationErrorsPage(),
        },
        new()
        {
            Title = "RefreshContainer",
            Content = new RefreshContainerPage(),
        },
        new()
        {
            Title = "PopConfirm",
            Content = new PopConfirmPage(),
        },
        new()
        {
            Title = "Skeleton",
            Content = new SkeletonPage(),
        },
        new()
        {
            Title = "Toast",
            Content = new ToastPage(),
        },
        new()
        {
            Title = "Notification",
            Content = new NotificationPage(),
        },
        new()
        {
            Title = "Loading",
            Content = new LoadingPage(),
        },
        new()
        {
            Title = "MessageBox",
            Content = new MessageBoxPage(),
        },
        new()
        {
            Title = "Dialog",
            Content = new DialogPage(),
        },
        new()
        {
            Title = "Drawer",
            Content = new DrawerPage(),
        },
        new()
        {
            Title = "ContentDialog",
            Content = new ContentDialogPage(),
        }
    ];
}