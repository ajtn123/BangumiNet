using BangumiNet.Api.Interfaces;
using BangumiNet.Api.P1.Models;
using BangumiNet.Models;

namespace BangumiNet.ViewModels;

public class CalendarViewModel : ViewModelBase
{
    [Obsolete("Use Api.P1.Models.Calendar from /p1/calendar instead.")]
    public CalendarViewModel(Api.Legacy.Calendar.Calendar calendar)
    {
        Source = calendar;
        DayOfWeek = Common.ParseDayOfWeek(calendar.Weekday?.Id);
        Weekday = calendar.Weekday;
        Subjects = calendar.Items?.Select(c => new SubjectViewModel(c)).ToObservableCollection();
    }

    public CalendarViewModel(KeyValuePair<DayOfWeek, IEnumerable<CalendarItem>> calendar)
    {
        Source = calendar;
        DayOfWeek = calendar.Key;
        Weekday = new WeekdayNames(calendar.Key);
        Subjects = calendar.Value?
            .Select(c => new SubjectViewModel(c.Subject!) { Collection = new CollectionStatusCounts() { Doing = c.Watchers } })
            .ToObservableCollection();
    }

    [Reactive] public object? Source { get; set; }
    [Reactive] public DayOfWeek? DayOfWeek { get; set; }
    [Reactive] public IWeekday? Weekday { get; set; }
    [Reactive] public ObservableCollection<SubjectViewModel>? Subjects { get; set; }

    public bool IsToday => DayOfWeek == DateTime.Today.DayOfWeek;
}
