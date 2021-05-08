using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace epj.CircularGauge
{
    public class CircularGaugeCanvasView : SKCanvasView
    {
        internal float StartAngle { get; set; } = 135.0f;
        internal float SweepAngle { get; set; } = 270.0f;
        internal float GaugeWidth { get; set; } = 40.0f;
        internal Color GaugeColor { get; set; } = Color.Red;

        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            var info = e.Info;
            var surface = e.Surface;
            var canvas = surface.Canvas;

            canvas.Clear();

            var rect = new SKRect(100, 100, info.Width - 100, info.Height - 100);

            using (var path = new SKPath())
            {
                path.AddArc(rect, StartAngle, SweepAngle);
                canvas.DrawPath(path, new SKPaint
                {
                    Color = GaugeColor.ToSKColor(),
                    IsAntialias = true,
                    Style = SKPaintStyle.Stroke,
                    StrokeWidth = GaugeWidth
                });
            }
        }
    }
}
