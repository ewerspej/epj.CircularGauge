using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace epj.CircularGauge
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CircularGauge : ContentView
    {
        private const float DecimalDelta = 0.001f;

        #region Properties
        public float StartAngle
        {
            get => GaugeCanvas.StartAngle;
            set
            {
                if (!(Math.Abs(value - GaugeCanvas.StartAngle) > DecimalDelta))
                {
                    return;
                }

                GaugeCanvas.StartAngle = value;
                GaugeCanvas.InvalidateSurface();
            }
        }

        public float SweepAngle
        {
            get => GaugeCanvas.SweepAngle;
            set
            {
                if (!(Math.Abs(value - GaugeCanvas.SweepAngle) > DecimalDelta))
                {
                    return;
                }

                GaugeCanvas.SweepAngle = value;
                GaugeCanvas.InvalidateSurface();
            }
        }

        public float GaugeWidth
        {
            get => GaugeCanvas.GaugeWidth;
            set
            {
                if (!(Math.Abs(value - GaugeCanvas.GaugeWidth) > DecimalDelta))
                {
                    return;
                }

                GaugeCanvas.GaugeWidth = value;
                GaugeCanvas.InvalidateSurface();
            }
        }

        public Color GaugeColor
        {
            get => GaugeCanvas.GaugeColor;
            set
            {
                if (value == GaugeCanvas.GaugeColor)
                {
                    return;
                }

                GaugeCanvas.GaugeColor = value;
                GaugeCanvas.InvalidateSurface();
            }
        }

        public List<Color> GaugeGradientColors
        {
            get => GaugeCanvas.GaugeGradientColors;
            set
            {
                if (value == GaugeCanvas.GaugeGradientColors)
                {
                    return;
                }

                GaugeCanvas.GaugeGradientColors = value;
                GaugeCanvas.InvalidateSurface();
            }
        }

        public float RangeStart
        {
            get => GaugeCanvas.RangeStart;
            set
            {
                if (Math.Abs(GaugeCanvas.RangeStart - value) < DecimalDelta)
                {
                    return;
                }

                GaugeCanvas.RangeStart = value;
                GaugeCanvas.InvalidateSurface();
            }
        }

        public float RangeEnd
        {
            get => GaugeCanvas.RangeEnd;
            set
            {
                if (Math.Abs(GaugeCanvas.RangeEnd - value) < DecimalDelta)
                {
                    return;
                }

                GaugeCanvas.RangeEnd = value;
                GaugeCanvas.InvalidateSurface();
            }
        }

        public float Value
        {
            get => GaugeCanvas.Value;
            set
            {
                if (Math.Abs(GaugeCanvas.Value - value) < DecimalDelta)
                {
                    return;
                }

                GaugeCanvas.Value = value;
                GaugeCanvas.InvalidateSurface();
            }
        }

        private double _size;
        public double Size
        {
            get => _size;
            set
            {
                if (Math.Abs(_size - value) < DecimalDelta)
                {
                    return;
                }

                _size = value;
                WidthRequest = HeightRequest = _size;
                GaugeCanvas.InvalidateSurface();
            }
        }

        #endregion

        #region Bindable Properties

        public static BindableProperty StartAngleProperty = BindableProperty.Create(propertyName: nameof(StartAngle),
                                                                                    returnType: typeof(float),
                                                                                    declaringType: typeof(CircularGauge),
                                                                                    defaultBindingMode: BindingMode.OneWay,
                                                                                    propertyChanged: OnStartAnglePropertyChanged);

        public static BindableProperty SweepAngleProperty = BindableProperty.Create(propertyName: nameof(SweepAngle),
                                                                                    returnType: typeof(float),
                                                                                    declaringType: typeof(CircularGauge),
                                                                                    defaultBindingMode: BindingMode.OneWay,
                                                                                    propertyChanged: OnSweepAnglePropertyChanged);

        public static BindableProperty GaugeWidthProperty = BindableProperty.Create(propertyName: nameof(GaugeWidth),
                                                                                    returnType: typeof(float),
                                                                                    declaringType: typeof(CircularGauge),
                                                                                    defaultBindingMode: BindingMode.OneWay,
                                                                                    propertyChanged: OnGaugeWidthPropertyChanged);

        public static BindableProperty GaugeColorProperty = BindableProperty.Create(propertyName: nameof(GaugeColor),
                                                                                    returnType: typeof(Color),
                                                                                    declaringType: typeof(CircularGauge),
                                                                                    defaultBindingMode: BindingMode.OneWay,
                                                                                    propertyChanged: OnGaugeColorPropertyChanged);

        public static BindableProperty RangeStartProperty = BindableProperty.Create(propertyName: nameof(RangeStart),
                                                                                    returnType: typeof(float),
                                                                                    declaringType: typeof(CircularGauge),
                                                                                    defaultBindingMode: BindingMode.OneWay,
                                                                                    propertyChanged: OnRangeStartPropertyChanged);

        public static BindableProperty RangeEndProperty = BindableProperty.Create(propertyName: nameof(RangeEnd),
                                                                                  returnType: typeof(float),
                                                                                  declaringType: typeof(CircularGauge),
                                                                                  defaultBindingMode: BindingMode.OneWay,
                                                                                  propertyChanged: OnRangeEndPropertyChanged);

        public static BindableProperty ValueProperty = BindableProperty.Create(propertyName: nameof(Value),
                                                                               returnType: typeof(float),
                                                                               declaringType: typeof(CircularGauge),
                                                                               defaultBindingMode: BindingMode.OneWay,
                                                                               propertyChanged: OnValuePropertyChanged);

        public static BindableProperty SizeProperty = BindableProperty.Create(propertyName: nameof(Size),
                                                                              returnType: typeof(double),
                                                                              declaringType: typeof(CircularGauge),
                                                                              defaultBindingMode: BindingMode.OneWay,
                                                                              propertyChanged: OnSizePropertyChanged);

        private static void OnStartAnglePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((CircularGauge)bindable).StartAngle = (float)newValue;
        }

        private static void OnSweepAnglePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((CircularGauge)bindable).SweepAngle = (float)newValue;
        }

        private static void OnGaugeWidthPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((CircularGauge)bindable).GaugeWidth = (float)newValue;
        }

        private static void OnGaugeColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((CircularGauge)bindable).GaugeColor = (Color)newValue;
        }

        private static void OnRangeEndPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((CircularGauge)bindable).RangeEnd = (float)newValue;
        }

        private static void OnRangeStartPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((CircularGauge)bindable).RangeStart = (float)newValue;
        }

        private static void OnValuePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((CircularGauge)bindable).Value = (float)newValue;
        }

        private static void OnSizePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((CircularGauge)bindable).Size = (double)newValue;
        }

        #endregion

        public CircularGauge()
        {
            InitializeComponent();
        }
    }
}