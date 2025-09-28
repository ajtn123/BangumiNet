using BangumiNet.Api.Legacy.Calendar;

namespace BangumiNet.ViewModels;

public partial class AiringViewModel : ViewModelBase
{
    public AiringViewModel(IEnumerable<Calendar> calendars)
    {
        Calendars = [.. calendars.Select(calendar => new CalendarViewModel(calendar))];
    }

    [Reactive] public partial ObservableCollection<CalendarViewModel>? Calendars { get; set; }
}
