using System;
using System.Collections.Generic;
using System.Linq;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace epj.CircularGauge
{
    internal sealed class CircularGaugeCanvasView : SKCanvasView
    {
        #region Private Fields

        private SKImageInfo _info;
        private SKSurface _surface;
        private SKCanvas _canvas;
        private SKRect _drawRect;
        private SKPoint _center;

        #endregion

        #region Properties
        internal float StartAngle { get; set; } = 45.0f;
        internal float SweepAngle { get; set; } = 270.0f;
        internal float GaugeWidth { get; set; } = 40.0f;
        internal Color GaugeColor { get; set; } = Color.Red;
        internal List<Color> GaugeGradientColors { get; set; } = new List<Color>();
        internal float RangeStart { get; set; } = 0.0f;
        internal float RangeEnd { get; set; } = 100.0f;
        internal float Value { get; set; } = 50.0f;

        #endregion

        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            _info = e.Info;
            _surface = e.Surface;
            _canvas = _surface.Canvas;
            _canvas.Clear();

            //setup the rectangle which we will draw in and the center point of the gauge
            _drawRect = new SKRect(100, 100, _info.Width - 100, _info.Height - 100);
            _center = new SKPoint(_info.Rect.MidX, _info.Rect.MidY);

            //the coordinate system of SkiaSharp starts with 0 degrees at 3 o'clock (polar coordinates),
            //but we want 0 degrees at 6 o'clock, so we rotate everything by 90 degrees.
            var startAngle90 = StartAngle + 90.0f;

            DrawGauge(startAngle90);
            //DrawScale();
            DrawNeedle();
        }

        private void DrawNeedle()
        {
            //TODO: implement
        }

        private void DrawScale()
        {
            //TODO: implement
            throw new NotImplementedException();
        }

        private void DrawGauge(float startAngle)
        {
            //draw gauge base
            using (var path = new SKPath())
            {
                path.AddArc(_drawRect, startAngle, SweepAngle);

                using (var paint = new SKPaint())
                {
                    if (GaugeGradientColors?.Count > 0)
                    {
                        var colors = GaugeGradientColors.Select(color => color.ToSKColor()).ToArray();

                        paint.Shader = SKShader.CreateSweepGradient(center: _center, colors: colors,
                                tileMode: SKShaderTileMode.Clamp, startAngle: 0.0f, endAngle: SweepAngle)
                            .WithLocalMatrix(SKMatrix.CreateRotationDegrees(startAngle, _center.X, _center.Y));
                    }
                    else
                    {
                        paint.Color = GaugeColor.ToSKColor();
                    }

                    paint.IsAntialias = true;
                    paint.Style = SKPaintStyle.Stroke;
                    paint.StrokeWidth = GaugeWidth;
                    _canvas.DrawPath(path, paint);
                }
            }
        }
    }
}
