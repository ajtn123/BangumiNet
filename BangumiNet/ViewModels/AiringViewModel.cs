using BangumiNet.Api.P1.Models;

namespace BangumiNet.ViewModels;

public partial class AiringViewModel : ViewModelBase
{
    [Obsolete]
    public AiringViewModel(IEnumerable<Api.Legacy.Calendar.Calendar> calendars)
    {
        Calendars = [.. calendars.Select(calendar => new CalendarViewModel(calendar))];
    }
    public AiringViewModel(Calendar calendar)
    {
        Calendars = [.. calendar.Days.Select(calendar => new CalendarViewModel(calendar))];
    }

    [Reactive] public partial ObservableCollection<CalendarViewModel>? Calendars { get; set; }
}
