using ReactiveUI.SourceGenerators;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BangumiNet.ViewModels;

public partial class AiringViewModel : ViewModelBase
{
    public AiringViewModel() => _ = Init();
    private async Task Init()
    {
        Calendars = await ApiC.Clients.LegacyClient.Calendar.GetAsync();
    }

    [Reactive] public partial List<Api.Legacy.Calendar.Calendar>? Calendars { get; set; }
}
