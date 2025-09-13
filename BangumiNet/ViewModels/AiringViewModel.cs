using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI.SourceGenerators;

namespace BangumiNet.ViewModels;

public partial class AiringViewModel:ViewModelBase
{
    public AiringViewModel() => _ = Init();
    private async Task Init()
    {
        Calendars = await ApiC.Clients.LegacyClient.Calendar.GetAsync();
    }

    [Reactive] public partial List<Api.Legacy.Calendar.Calendar>? Calendars { get; set; }
}
