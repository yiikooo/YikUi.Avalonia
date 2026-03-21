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
            Title = "DatePicker(yik)",
            Content = new DatePickerYikPage(),
        },
        new()
        {
            Title = "TimePicker",
            Content = new TimePickerPage(),
        },
        new()
        {
            Title = "Clock",
            Content = new ClockPage(),
        },
        new()
        {
            Title = "DateRangePicker",
            Content = new DateRangePickerPage(),
        },
        new()
        {
            Title = "DateTimePicker",
            Content = new DateTimePickerPage(),
        },
        new()
        {
            Title = "TimeBox",
            Content = new TimeBoxPage(),
        },
        new()
        {
            Title = "TimePicker(yik)",
            Content = new TimePickerYikPage(),
        },
        new()
        {
            Title = "TimeRangePicker",
            Content = new TimeRangePickerPage(),
        },
    ];
}