using System.Collections.Generic;
using YikUi.Demo.Pages;

namespace YikUi.Demo.Navs;

public static class Times
{
    public static readonly List<Page> TimesList =
    [
        new()
        {
            Title = "Calendar",
            Content = new CalendarPage(),
        },
        new()
        {
            Title = "CalendarDatePicker",
            Content = new CalendarDatePickerPage(),
        },
        new()
        {
            Title = "DatePicker",
            Content = new DatePickerPage(),
        },
        new()
        {
            Title = "TimePicker",
            Content = new TimePickerPage(),
        },
    ];
}