using Avalonia.ReactiveUI;
using BangumiNet.Api.V0.Models;
using BangumiNet.ViewModels;

namespace BangumiNet.Views;

public partial class PersonView : ReactiveUserControl<PersonViewModel>
{
    public PersonView()
    {
        InitializeComponent();

        DataContextChanged += async (s, e) =>
        {
            if (dataContextChanges >= 1) return;
            if (DataContext is not PersonViewModel viewModel) return;
            if (!viewModel.IsFull)
            {
                if (viewModel.Id is not int id) return;
                var fullPerson = await ApiC.V0.Persons[id].GetAsync();
                if (fullPerson == null) return;
                dataContextChanges += 1;
                var vm = new PersonViewModel(fullPerson);
                DataContext = vm;
            }
        };
    }

    private uint dataContextChanges = 0;
}