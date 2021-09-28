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
        #region Default Values

        private const float DefaultStartAngle = 45.0f;
        private const float DefaultSweepAngle = 270.0f;
        private const float DefaultGaugeWidth = 10.0f;
        private const float DefaultRangeStart = 0.0f;
        private const float DefaultRangeEnd = 100.0f;
        private const float DefaultValue = 50.0f;
        private const float DefaultNeedleLength = 128.0f;
        private const float DefaultNeedleWidth = 18.0f;
        private const float DefaultNeedleOffset = 18.0f;
        private const float DefaultBaseWidth = 24.0f;
        private const float DefaultBaseStrokeWidth = 4.0f;
        private const int DefaultSize = 250;

        #endregion

        #region Private Fields

        private SKImageInfo _info;
        private SKSurface _surface;
        private SKCanvas _canvas;
        private SKPoint _center;
        private SKRect _drawRect;
        private float _adjustedStartAngle;

        //paint for the major units
        private readonly SKPaint _majorUnitsPaint = new SKPaint
        {
            Color = SKColors.Black
        };

        #endregion

        #region Properties

        internal float StartAngle { get; set; } = DefaultStartAngle;
        internal float SweepAngle { get; set; } = DefaultSweepAngle;
        internal float GaugeWidth { get; set; } = DefaultGaugeWidth;
        internal Color GaugeColor { get; set; } = Color.Red;
        internal List<Color> GaugeGradientColors { get; set; } = new List<Color>();
        internal float RangeStart { get; set; } = DefaultRangeStart;
        internal float RangeEnd { get; set; } = DefaultRangeEnd;
        internal float Value { get; set; } = DefaultValue;
        internal Color NeedleColor { get; set; } = Color.Black;
        internal float NeedleLength { get; set; } = DefaultNeedleLength;
        internal float NeedleWidth { get; set; } = DefaultNeedleWidth;
        internal float NeedleOffset { get; set; } = DefaultNeedleOffset;
        internal Color BaseColor { get; set; } = Color.Black;
        internal float BaseWidth { get; set; } = DefaultBaseWidth;
        internal Color BaseStrokeColor { get; set; } = Color.DimGray;
        internal float BaseStrokeWidth { get; set; } = DefaultBaseStrokeWidth;
        internal bool DrawBaseStrokeBeforeFill { get; set; } = false;
        internal int Size { get; set; } = DefaultSize;
        internal float InternalPadding => 10.0f;

        #endregion

        #region Constructor

        public CircularGaugeCanvasView()
        {
            IgnorePixelScaling = false;
        }

        #endregion

        #region SKCanvasView Overrides

        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            _info = e.Info;
            _surface = e.Surface;
            _canvas = _surface.Canvas;
            _canvas.Clear();

            Size = Math.Min(_info.Size.Width, _info.Size.Height);

            //offsets are used to always center the gauge inside the canvas
            var horizontalOffset = (_info.Size.Width - Size) / 2;
            var verticalOffset = (_info.Size.Height - Size) / 2;

            //setup the rectangle which we will draw in and the center point of the gauge
            _drawRect = new SKRect(0 + InternalPadding + horizontalOffset, 0 + InternalPadding + verticalOffset, Size - (InternalPadding - horizontalOffset), Size - (InternalPadding - verticalOffset));
            _center = new SKPoint(_drawRect.MidX, _drawRect.MidY);

            //the coordinate system of SkiaSharp starts with 0 degrees at 3 o'clock (polar coordinates),
            //but we want 0 degrees at 6 o'clock, so we rotate everything by 90 degrees.
            _adjustedStartAngle = StartAngle + 90.0f;

            OnDrawGauge();
            OnDrawScale();
            OnDrawNeedle();
            OnDrawNeedleBase();
        }

        #endregion

        #region Private Methods

        private void OnDrawNeedle()
        {
            using (var needlePath = new SKPath())
            {
                //first set up needle pointing towards 0 degrees (or 6 o'clock)
                var widthOffset = ScaleToSize(NeedleWidth / 2.0f);
                var needleOffset = ScaleToSize(NeedleOffset);
                var needleStart = _center.Y - needleOffset;
                var needleLength = ScaleToSize(NeedleLength);

                needlePath.MoveTo(_center.X - widthOffset, needleStart);
                needlePath.LineTo(_center.X + widthOffset, needleStart);
                needlePath.LineTo(_center.X, needleStart + needleLength);
                needlePath.LineTo(_center.X - widthOffset, needleStart);
                needlePath.Close();

                //then calculate needle position in degrees
                var needlePosition = StartAngle + ((Value - RangeStart) / (RangeEnd - RangeStart) * SweepAngle);

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

        private void OnDrawNeedleBase()
        {
            using (var basePath = new SKPath())
            {
                var baseRadius = ScaleToSize(BaseWidth / 2.0f);

                basePath.AddCircle(_center.X, _center.Y, baseRadius);

                var basePaint = new SKPaint();
                var baseStrokePaint = new SKPaint();

                try
                {
                    basePaint.IsAntialias = true;
                    basePaint.Color = BaseColor.ToSKColor();
                    basePaint.Style = SKPaintStyle.Fill;

                    baseStrokePaint.IsAntialias = true;
                    baseStrokePaint.Color = BaseStrokeColor.ToSKColor();
                    baseStrokePaint.StrokeWidth = ScaleToSize(BaseStrokeWidth);
                    baseStrokePaint.Style = SKPaintStyle.Stroke;

                    if (DrawBaseStrokeBeforeFill)
                    {
                        _canvas.DrawPath(basePath, baseStrokePaint);
                        _canvas.DrawPath(basePath, basePaint);
                    }
                    else
                    {
                        _canvas.DrawPath(basePath, basePaint);
                        _canvas.DrawPath(basePath, baseStrokePaint);
                    }
                }
                finally
                {
                    basePaint?.Dispose();
                    baseStrokePaint?.Dispose();
                }
            }
        }

        private void OnDrawScale()
        {
            using (var scalePath = new SKPath())
            {
                //the arc path to draw the scale along
                var gaugeRect = GetGaugeRect();
                scalePath.AddArc(gaugeRect, _adjustedStartAngle, SweepAngle);

                //TODO: only for testing purposes, remove later!
                _canvas.DrawPath(scalePath, new SKPaint
                {
                    Style = SKPaintStyle.Stroke,
                    StrokeWidth = 10,
                    Color = SKColors.DarkGray
                });

                //template path for the major scale units
                var majorUnitsPath = new SKPath();
                majorUnitsPath.MoveTo(-5, -40);
                majorUnitsPath.LineTo(-5, 40);
                majorUnitsPath.LineTo(5, 40);
                majorUnitsPath.LineTo(5, -40);
                majorUnitsPath.Close();

                //calculate length of arc path
                var radius = gaugeRect.Width / 2;
                var circumference = 2 * (float)Math.PI * radius;
                var length = circumference * (SweepAngle / 360);

                //calculate spacing based on length of arc path
                var spacing = (float)Math.Round(length / 10); //we just use ten major units for now

                //use SKPathEffect to draw major units along arc path
                using (var majorUnitsPathEffect =
                    SKPathEffect.Create1DPath(majorUnitsPath, spacing, 0, SKPath1DPathEffectStyle.Rotate))
                {
                    _majorUnitsPaint.PathEffect = majorUnitsPathEffect;
                    _canvas.DrawPath(scalePath, _majorUnitsPaint);
                }
            }
        }

        private void OnDrawGauge()
        {
            //draw gauge base
            using (var path = new SKPath())
            {
                path.AddArc(GetGaugeRect(), _adjustedStartAngle, SweepAngle);

                using (var paint = new SKPaint())
                {
                    if (GaugeGradientColors?.Count > 0)
                    {
                        var colors = GaugeGradientColors.Select(color => color.ToSKColor()).ToArray();

                        paint.Shader = SKShader.CreateSweepGradient(_center, colors, SKShaderTileMode.Decal, 0.0f, SweepAngle)
                                               .WithLocalMatrix(SKMatrix.CreateRotationDegrees(_adjustedStartAngle, _center.X, _center.Y));
                    }
                    else
                    {
                        paint.Color = GaugeColor.ToSKColor();
                    }

                    paint.IsAntialias = true;
                    paint.Style = SKPaintStyle.Stroke;
                    paint.StrokeWidth = GaugeWidth / DefaultSize * Size;
                    _canvas.DrawPath(path, paint);
                }
            }
        }

        private SKRect GetGaugeRect()
        {
            var gaugePadding = GaugeWidth / 2.0f / DefaultSize * Size;
            var gaugeRect = new SKRect(_drawRect.Left + gaugePadding, _drawRect.Top + gaugePadding,
                _drawRect.Right - gaugePadding, _drawRect.Bottom - gaugePadding);
            return gaugeRect;
        }

        #endregion

        #region Helpers

        private float ScaleToSize(float value) => value / DefaultSize * Size;

        #endregion
    }
}
