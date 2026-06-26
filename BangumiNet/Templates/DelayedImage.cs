using Avalonia.Controls;
using Avalonia.Media.Imaging;

namespace BangumiNet.Templates;

public partial class DelayedImage : Image
{
    public DelayedImage(Task<Bitmap?> source)
    {
        Task.Run(async () =>
        {
            var image = await source;

            Dispatcher.Post(() =>
            {
                Source = image;
            });
        });
    }
}
