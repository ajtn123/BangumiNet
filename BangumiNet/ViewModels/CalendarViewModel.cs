using BangumiNet.Api.Interfaces;
using BangumiNet.Api.Legacy.Calendar;
using BangumiNet.Utils;
using ReactiveUI.SourceGenerators;
using System;
using System.Collections.ObjectModel;
using System.Linq;

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
}
