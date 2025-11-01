using LiveChartsCore.Drawing;
using LiveChartsCore.SkiaSharpView.Drawing;

namespace BangumiNet.Utils;

public class VoidGeometry() : BoundedDrawnGeometry, IDrawnElement<SkiaSharpDrawingContext>
{ public void Draw(SkiaSharpDrawingContext context) { } }
