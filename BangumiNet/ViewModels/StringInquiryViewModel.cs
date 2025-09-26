using System.Windows.Input;

namespace BangumiNet.ViewModels;

public partial class StringInquiryViewModel : ViewModelBase
{
    public StringInquiryViewModel()
    {
        ConfirmCommand = ReactiveCommand.Create<string?>(() => { return Result; });
        CancelCommand = ReactiveCommand.Create<string?>(() => { return null; });
    }

    [Reactive] public partial string? Result { get; set; }
    [Reactive] public partial string? Watermark { get; set; }

    public ICommand ConfirmCommand { get; set; }
    public ICommand CancelCommand { get; set; }
}
