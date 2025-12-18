using BangumiNet.Api.ExtraEnums;
using BangumiNet.Common;

namespace BangumiNet.Views;

public partial class SubjectCollectionListView : ReactiveUserControl<SubjectCollectionListViewModel>
{
    public SubjectCollectionListView()
    {
        InitializeComponent();
        ItemTypeComboBox.ItemsSource = (ItemType[])[ItemType.Subject, ItemType.Character, ItemType.Person, ItemType.Index];
        SubjectTypeComboBox.ItemsSource = (SubjectType?[])[null, .. Enum.GetValues<SubjectType>()];
        CollectionTypeComboBox.ItemsSource = (CollectionType?[])[null, .. Enum.GetValues<CollectionType>()];
    }
}