using BangumiNet.Api.ExtraEnums;
using BangumiNet.Api.P1.Models;
using BangumiNet.Common;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace BangumiNet.ViewModels;

public partial class AiringViewModel : ViewModelBase, IActivatableViewModel
{
    public AiringViewModel()
    {
        this.WhenActivated(async (CompositeDisposable disposables) =>
        {
            if (Calendars == null)
            {
                await LoadAsync();
                await Highlight();
            }
        });
    }

    private async Task LoadAsync()
    {
        Calendar? response = null;
        try
        {
            response = await ApiC.P1.Calendar.GetAsync();
        }
        catch (Exception e) { Trace.TraceError(e.ToString()); }
        if (response is null) return;

        Calendars = response.Days.Select(calendar => new CalendarViewModel(calendar)).ToObservableCollection();
    }

    private async Task Highlight()
    {
        var cl = new SubjectCollectionListViewModel(ItemType.Subject, SubjectType.Anime, CollectionType.Doing);
        IDisposable? disposable = null;
        cl.WhenAnyValue(x => x.SubjectViewModels).WhereNotNull().Take(1).Subscribe(s =>
        {
            var cids = s.Select(s => (s as SubjectCollectionViewModel)?.Parent?.Id).ToArray();
            if (cids == null || Calendars == null) return;
            foreach (var subject in Calendars.SelectMany(x => x.Subjects ?? []))
                if (cids.Contains(subject.Id)) subject.IsHighlighted = true;
            disposable?.Dispose();
        });
        disposable = cl.Activator.Activate();
    }

    [Reactive] public partial ObservableCollection<CalendarViewModel>? Calendars { get; set; }

    public ViewModelActivator Activator { get; } = new();
}
