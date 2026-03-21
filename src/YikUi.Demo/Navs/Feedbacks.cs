using System.Collections.Generic;
using YikUi.Demo.Pages;

namespace YikUi.Demo.Navs;

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
            Title = "Toast",
            Content = new ToastPage(),
        },
        new()
        {
            Title = "Notification",
            Content = new NotificationPage(),
        }
    ];
}