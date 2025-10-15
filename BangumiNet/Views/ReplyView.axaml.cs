namespace BangumiNet.Views;

public partial class ReplyView : ReactiveUserControl<ReplyViewModel>
{
    public ReplyView()
    {
        InitializeComponent();

        DataContextChanged += (s, e) =>
        {
            getTurnstileInteractionHandler?.Dispose();
            getTurnstileInteractionHandler = ViewModel?.GetTurnstileInteraction.RegisterHandler(async i =>
            {
                var tv = new TurnstileView();
                MainVertical.Children.Add(tv);

                var token = await tv.GetToken();
                i.SetOutput(token);

                MainVertical.Children.RemoveAll(MainVertical.Children.Where(c => c is TurnstileView));
            });
        };
    }

    private IDisposable? getTurnstileInteractionHandler;
}
