namespace BangumiNet.Views;

public partial class TopicView : ReactiveUserControl<TopicViewModel>
{
    public TopicView()
    {
        InitializeComponent();

        DataContextChanged += async (s, e) =>
        {
            if (dataContextChanges >= 1) return;
            if (DataContext is not TopicViewModel viewModel) return;
            if (!viewModel.IsFull)
            {
                if (viewModel.Id is not int id || viewModel.ParentType is not ItemType parentType) return;
                var fullSubject = await ApiC.GetTopicViewModelAsync(parentType, id);
                if (fullSubject == null) return;
                dataContextChanges += 1;
                DataContext = fullSubject;
            }
        };
    }

    private uint dataContextChanges = 0;
}