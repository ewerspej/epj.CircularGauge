using System.Collections.Generic;
using System.Linq;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace epj.CircularGauge
{
    public class CircularGaugeCanvasView : SKCanvasView
    {
        internal float StartAngle { get; set; } = 45.0f;
        internal float SweepAngle { get; set; } = 270.0f;
        internal float GaugeWidth { get; set; } = 40.0f;
        internal Color GaugeColor { get; set; } = Color.Red;
        internal List<Color> GaugeGradientColors { get; set; } = new List<Color>();

        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            var info = e.Info;
            var surface = e.Surface;
            var canvas = surface.Canvas;

            canvas.Clear();

            var rect = new SKRect(100, 100, info.Width - 100, info.Height - 100);

            using (var path = new SKPath())
            {
                var startAngle90 = StartAngle + 90.0f;

                path.AddArc(rect, startAngle90, SweepAngle);

                using (var paint = new SKPaint())
                {
                    if (GaugeGradientColors?.Count > 0)
                    {
                        var colors = GaugeGradientColors.Select(color => color.ToSKColor()).ToArray();
                        var center = new SKPoint(info.Rect.MidX, info.Rect.MidY);

                        paint.Shader = SKShader.CreateSweepGradient(center: center, colors: colors, tileMode: SKShaderTileMode.Clamp, startAngle: 0.0f, endAngle: SweepAngle)
                                               .WithLocalMatrix(SKMatrix.CreateRotationDegrees(startAngle90, center.X, center.Y));

                    }
                    else
                    {
                        paint.Color = GaugeColor.ToSKColor();
                    }

                    paint.IsAntialias = true;
                    paint.Style = SKPaintStyle.Stroke;
                    paint.StrokeWidth = GaugeWidth;
                    canvas.DrawPath(path, paint);
                }
            }
        }
    }
}
