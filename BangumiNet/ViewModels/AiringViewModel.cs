using BangumiNet.Api.P1.Models;

namespace BangumiNet.ViewModels;

public partial class AiringViewModel : ViewModelBase
{
    [Obsolete("Use Api.P1.Models.Calendar from /p1/calendar instead.")]
    public AiringViewModel(IEnumerable<Api.Legacy.Calendar.Calendar> calendars)
    {
        Calendars = [.. calendars.Select(calendar => new CalendarViewModel(calendar))];
    }
    public AiringViewModel(Calendar calendar)
    {
        foreach (var day in calendar.AdditionalData)
        {
            Console.WriteLine("");
            //SlimUser.CreateFromDiscriminatorValue(day.Value);
        }
    }

    [Reactive] public partial ObservableCollection<CalendarViewModel>? Calendars { get; set; }
}
