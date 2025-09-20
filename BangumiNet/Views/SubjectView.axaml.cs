using Avalonia.ReactiveUI;
using BangumiNet.Utils;
using BangumiNet.ViewModels;

namespace BangumiNet.Views;

public partial class SubjectView : ReactiveUserControl<SubjectViewModel>
{
    public SubjectView()
    {
        InitializeComponent();

        DataContextChanged += async (s, e) =>
        {
            if (dataContextChanges > 10) return;
            if (DataContext is not SubjectViewModel viewModel) return;
            if (viewModel.Source?.GetType() == typeof(Api.V0.Models.Subject)) return;
            if (viewModel.Id is not int id) return;
            var fullSubject = await ApiC.V0.Subjects[id].GetAsync();
            if (fullSubject == null) return;
            dataContextChanges += 1;
            var vm = new SubjectViewModel(fullSubject);
            _ = vm.EpisodeListViewModel?.LoadEpisodes();
            DataContext = vm;
        };
    }

    private uint dataContextChanges = 0;
}