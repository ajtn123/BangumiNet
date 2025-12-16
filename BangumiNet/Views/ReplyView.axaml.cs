using System.Reactive.Disposables.Fluent;

namespace BangumiNet.Views;

public partial class ReplyView : ReactiveUserControl<ReplyViewModel>
{
    public ReplyView()
    {
        InitializeComponent();

        this.WhenActivated(disposables =>
        {
            this.WhenAnyValue(x => x.ViewModel)
                .Subscribe(vm =>
                {
                    getTurnstileInteractionHandler?.Dispose();
                    getTurnstileInteractionHandler = ViewModel?.GetTurnstileInteraction.RegisterHandler(i => i.SetOutput(null));
                    //getTurnstileInteractionHandler = ViewModel?.GetTurnstileInteraction.RegisterHandler(async i =>
                    //{
                    //    var tv = new TurnstileView();
                    //    MainVertical.Children.Add(tv);

                    //    var token = await tv.GetToken();
                    //    i.SetOutput(token);

                    //    MainVertical.Children.RemoveAll(MainVertical.Children.Where(c => c is TurnstileView));
                    //});
                }).DisposeWith(disposables);
        });
    }

    private IDisposable? getTurnstileInteractionHandler;
}
