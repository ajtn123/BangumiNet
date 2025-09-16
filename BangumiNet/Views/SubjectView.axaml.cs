using Avalonia.ReactiveUI;
using BangumiNet.ViewModels;

namespace BangumiNet.Views;

public partial class SubjectView : ReactiveUserControl<SubjectViewModel>
{
    public SubjectView()
    {
        InitializeComponent();

        DataContextChanged += async (s, e) =>
        {
            if (DataContext is not SubjectViewModel viewModel) return;
            if (viewModel.SourceType == typeof(Api.V0.Models.Subject)) return;
            if (viewModel.Id is not int id) return;
            var fullSubject = await ApiC.V0.Subjects[id].GetAsync();
            if (fullSubject == null) return;
            DataContext = new SubjectViewModel(fullSubject);
        };
    }
}