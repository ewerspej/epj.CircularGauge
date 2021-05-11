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
        private SKPoint _center;
        private SKRect _drawRect;
        private float _startAngle90;

        #endregion

        #region Properties

        internal float StartAngle { get; set; } = 45.0f;
        internal float SweepAngle { get; set; } = 270.0f;
        internal float GaugeWidth { get; set; } = 10.0f;
        internal Color GaugeColor { get; set; } = Color.Red;
        internal List<Color> GaugeGradientColors { get; set; } = new List<Color>();
        internal float RangeStart { get; set; } = 0.0f;
        internal float RangeEnd { get; set; } = 100.0f;
        internal float Value { get; set; } = 50.0f;
        internal Color NeedleColor { get; set; } = Color.Black;
        internal int Size { get; set; } = 250;
        internal float InternalPadding { get; private set; }

        #endregion

        #region Constructor

        public CircularGaugeCanvasView()
        {
            IgnorePixelScaling = true;
        }

        #endregion

        #region SKCanvasView Overrides

        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            _info = e.Info;
            _surface = e.Surface;
            _canvas = _surface.Canvas;
            _canvas.Clear();

            InternalPadding = 10.0f;

            //setup the rectangle which we will draw in and the center point of the gauge
            _drawRect = new SKRect(0 + InternalPadding, 0 + InternalPadding, Size - InternalPadding, Size - InternalPadding);
            _center = new SKPoint(_drawRect.MidX, _drawRect.MidY);

            //the coordinate system of SkiaSharp starts with 0 degrees at 3 o'clock (polar coordinates),
            //but we want 0 degrees at 6 o'clock, so we rotate everything by 90 degrees.
            _startAngle90 = StartAngle + 90.0f;

            DrawGauge();
            //DrawScale();
            DrawNeedle();
        }

        #endregion

        #region Private Methods

        private void DrawNeedle()
        {
            //first draw a circle as the base for the needle
            using (var basePath = new SKPath())
            {
                basePath.AddCircle(_center.X, _center.Y, _drawRect.Width * 0.05f);

                using (var basePaint = new SKPaint())
                {
                    basePaint.IsAntialias = true;
                    basePaint.Color = NeedleColor.ToSKColor();
                    basePaint.Style = SKPaintStyle.Fill;
                    _canvas.DrawPath(basePath, basePaint);
                }
            }

            using (var needlePath = new SKPath())
            {
                //first set up needle pointing towards 0 degrees (or 6 o'clock)
                needlePath.MoveTo(_center.X - _drawRect.Width * 0.035f, _center.Y - _drawRect.Width * 0.1f);
                needlePath.LineTo(_center.X + _drawRect.Width * 0.035f, _center.Y - _drawRect.Width * 0.1f);
                needlePath.LineTo(_center.X, _center.Y + _drawRect.Height * 0.55f);
                needlePath.LineTo(_center.X - _drawRect.Width * 0.035f, _center.Y - _drawRect.Width * 0.1f);
                needlePath.Close();

                //then calculate needle position in degrees
                var needlePosition = StartAngle + (Value / (RangeEnd - RangeStart) * SweepAngle);

                //finally rotate needle to actual value
                needlePath.Transform(SKMatrix.CreateRotationDegrees(needlePosition, _center.X, _center.Y));

                using (var needlePaint = new SKPaint())
                {
                    needlePaint.IsAntialias = true;
                    needlePaint.Color = NeedleColor.ToSKColor();
                    needlePaint.Style = SKPaintStyle.Fill;
                    _canvas.DrawPath(needlePath, needlePaint);
                }
            }
        }

        private void DrawScale()
        {
            //TODO: implement
            throw new NotImplementedException();
        }

        private void DrawGauge()
        {
            //draw gauge base
            using (var path = new SKPath())
            {
                var gaugePadding = GaugeWidth / 2.0f;
                var gaugeRect = new SKRect(_drawRect.Left + gaugePadding, _drawRect.Top + gaugePadding,
                    _drawRect.Right - gaugePadding, _drawRect.Bottom - gaugePadding);

                path.AddArc(gaugeRect, _startAngle90, SweepAngle);

                using (var paint = new SKPaint())
                {
                    if (GaugeGradientColors?.Count > 0)
                    {
                        var colors = GaugeGradientColors.Select(color => color.ToSKColor()).ToArray();

                        paint.Shader = SKShader.CreateSweepGradient(center: _center, colors: colors, tileMode: SKShaderTileMode.Decal, startAngle: 0.0f, endAngle: SweepAngle)
                                               .WithLocalMatrix(SKMatrix.CreateRotationDegrees(_startAngle90, _center.X, _center.Y));
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

        #endregion
    }
}
