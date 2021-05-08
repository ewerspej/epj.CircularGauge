using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace epj.CircularGauge
{
    public class CircularGaugeCanvasView : SKCanvasView
    {
        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            var info = e.Info;
            var surface = e.Surface;
            var canvas = surface.Canvas;

            canvas.Clear();

            var rect = new SKRect(100, 100, info.Width - 100, info.Height - 100);
            var startAngle = 45.0f;
            var sweepAngle = 90.0f;

            //canvas.DrawOval(rect, new SKPaint
            //{
            //    Color = Color.Black.ToSKColor(),
            //    IsAntialias = true,
            //    Style = SKPaintStyle.Fill
            //});

            using (var path = new SKPath())
            {
                path.AddArc(rect, startAngle, sweepAngle);
                canvas.DrawPath(path, new SKPaint
                {
                    Color = Color.Red.ToSKColor(),
                    IsAntialias = true,
                    Style = SKPaintStyle.Stroke,
                    StrokeWidth = 25
                });
            }
        }
    }
}
