using ReactiveUI.SourceGenerators;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace BangumiNet.ViewModels;

public partial class AiringViewModel : ViewModelBase
{
    public AiringViewModel() => _ = Init();
    private async Task Init()
    {
        Calendars = (await ApiC.Clients.LegacyClient.Calendar.GetAsync())?.Select(c => new CalendarViewModel(c)).ToObservableCollection();
    }

    [Reactive] public partial ObservableCollection<CalendarViewModel>? Calendars { get; set; }
}
