using BangumiNet.Api.Interfaces;
using BangumiNet.Api.Legacy.Calendar;

namespace BangumiNet.ViewModels;

public class CalendarViewModel : ViewModelBase
{
    public CalendarViewModel(Calendar calendar)
    {
        Calendar = calendar;
        DayOfWeek = Common.ParseDayOfWeek(calendar.Weekday?.Id);
        Weekday = calendar.Weekday;
        Subjects = calendar.Items?.Select(c => new SubjectViewModel(c)).ToObservableCollection();
    }

    [Reactive] public Calendar? Calendar { get; set; }
    [Reactive] public DayOfWeek? DayOfWeek { get; set; }
    [Reactive] public IWeekday? Weekday { get; set; }
    [Reactive] public ObservableCollection<SubjectViewModel>? Subjects { get; set; }

    public bool IsToday => DayOfWeek == DateTime.Today.DayOfWeek;
}
