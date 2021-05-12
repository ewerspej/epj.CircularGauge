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
                OnPropertyChanged();
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
                OnPropertyChanged();
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
                OnPropertyChanged();
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
                OnPropertyChanged();
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
                OnPropertyChanged();
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
                OnPropertyChanged();
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
                OnPropertyChanged();
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
                OnPropertyChanged();
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
                GaugeCanvas.Size = (int)Math.Floor(_size);
                GaugeCanvas.InvalidateSurface();
                OnPropertyChanged();
            }
        }

        public Color NeedleColor
        {
            get => GaugeCanvas.NeedleColor;
            set
            {
                if (value == GaugeCanvas.NeedleColor)
                {
                    return;
                }

                GaugeCanvas.NeedleColor = value;
                GaugeCanvas.InvalidateSurface();
                OnPropertyChanged();
            }
        }

        public float NeedleLength
        {
            get => GaugeCanvas.NeedleLength;
            set
            {
                if (Math.Abs(GaugeCanvas.NeedleLength - value) < DecimalDelta)
                {
                    return;
                }

                GaugeCanvas.NeedleLength = value;
                GaugeCanvas.InvalidateSurface();
                OnPropertyChanged();
            }
        }

        public float NeedleWidth
        {
            get => GaugeCanvas.NeedleWidth;
            set
            {
                if (Math.Abs(GaugeCanvas.NeedleWidth - value) < DecimalDelta)
                {
                    return;
                }

                GaugeCanvas.NeedleWidth = value;
                GaugeCanvas.InvalidateSurface();
                OnPropertyChanged();
            }
        }

        public float NeedleOffset
        {
            get => GaugeCanvas.NeedleOffset;
            set
            {
                if (Math.Abs(GaugeCanvas.NeedleOffset - value) < DecimalDelta)
                {
                    return;
                }

                GaugeCanvas.NeedleOffset = value;
                GaugeCanvas.InvalidateSurface();
                OnPropertyChanged();
            }
        }

        public Color BaseColor
        {
            get => GaugeCanvas.BaseColor;
            set
            {
                if (value == GaugeCanvas.BaseColor)
                {
                    return;
                }

                GaugeCanvas.BaseColor = value;
                GaugeCanvas.InvalidateSurface();
                OnPropertyChanged();
            }
        }

        public float BaseWidth
        {
            get => GaugeCanvas.BaseWidth;
            set
            {
                if (Math.Abs(GaugeCanvas.BaseWidth - value) < DecimalDelta)
                {
                    return;
                }

                GaugeCanvas.BaseWidth = value;
                GaugeCanvas.InvalidateSurface();
                OnPropertyChanged();
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

        public static BindableProperty NeedleColorProperty = BindableProperty.Create(propertyName: nameof(NeedleColor),
                                                                                     returnType: typeof(Color),
                                                                                     declaringType: typeof(CircularGauge),
                                                                                     defaultBindingMode: BindingMode.OneWay,
                                                                                     propertyChanged: OnNeedleColorPropertyChanged);


        public static BindableProperty NeedleLengthProperty = BindableProperty.Create(propertyName: nameof(NeedleLength),
                                                                                      returnType: typeof(float),
                                                                                      declaringType: typeof(CircularGauge),
                                                                                      defaultBindingMode: BindingMode.OneWay,
                                                                                      propertyChanged: OnNeedleLengthPropertyChanged);

        public static BindableProperty NeedleWidthProperty = BindableProperty.Create(propertyName: nameof(NeedleWidth),
                                                                                     returnType: typeof(float),
                                                                                     declaringType: typeof(CircularGauge),
                                                                                     defaultBindingMode: BindingMode.OneWay,
                                                                                     propertyChanged: OnNeedleWidthPropertyChanged);

        public static BindableProperty NeedleOffsetProperty = BindableProperty.Create(propertyName: nameof(NeedleOffset),
                                                                                      returnType: typeof(float),
                                                                                      declaringType: typeof(CircularGauge),
                                                                                      defaultBindingMode: BindingMode.OneWay,
                                                                                      propertyChanged: OnNeedleOffsetPropertyChanged);

        public static BindableProperty BaseColorProperty = BindableProperty.Create(propertyName: nameof(BaseColor),
                                                                                   returnType: typeof(Color),
                                                                                   declaringType: typeof(CircularGauge),
                                                                                   defaultBindingMode: BindingMode.OneWay,
                                                                                   propertyChanged: OnBaseColorPropertyChanged);

        public static BindableProperty BaseWidthProperty = BindableProperty.Create(propertyName: nameof(BaseWidth),
                                                                                   returnType: typeof(float),
                                                                                   declaringType: typeof(CircularGauge),
                                                                                   defaultBindingMode: BindingMode.OneWay,
                                                                                   propertyChanged: OnBaseWidthPropertyChanged);

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

        private static void OnNeedleColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((CircularGauge)bindable).NeedleColor = (Color)newValue;
        }

        private static void OnNeedleLengthPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((CircularGauge)bindable).NeedleLength = (float)newValue;
        }

        private static void OnNeedleWidthPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((CircularGauge)bindable).NeedleWidth = (float)newValue;
        }

        private static void OnNeedleOffsetPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((CircularGauge)bindable).NeedleOffset = (float)newValue;
        }

        private static void OnBaseColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((CircularGauge)bindable).BaseColor = (Color)newValue;
        }

        private static void OnBaseWidthPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((CircularGauge)bindable).BaseWidth = (float)newValue;
        }

        #endregion

        public CircularGauge()
        {
            InitializeComponent();
        }
    }
}