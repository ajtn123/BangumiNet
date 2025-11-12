using BangumiNet.Api.ExtraEnums;
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
        Task.Run(async () =>
        {
            var cl = new SubjectCollectionListViewModel(ItemType.Subject, SubjectType.Anime, CollectionType.Doing);
            await cl.Load();
            var cids = cl.SubjectList.SubjectViewModels?.Select(s => (s as SubjectCollectionViewModel)?.Subject?.Id).ToArray();
            if (cids == null) return;
            foreach (var subject in Calendars.SelectMany(x => x.Subjects ?? []))
                if (cids.Contains(subject.Id)) subject.IsEmphasized = true;
        });
    }

    [Reactive] public partial ObservableCollection<CalendarViewModel>? Calendars { get; set; }
}
