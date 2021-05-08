using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace epj.CircularGauge
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CircularGauge : ContentView
    {
        private const float FloatDelta = 0.001f;

        #region Properties
        public float StartAngle
        {
            get => GaugeCanvas.StartAngle;
            set
            {
                if (!(Math.Abs(value - GaugeCanvas.StartAngle) > FloatDelta))
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
                if (!(Math.Abs(value - GaugeCanvas.SweepAngle) > FloatDelta))
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
                if (!(Math.Abs(value - GaugeCanvas.GaugeWidth) > FloatDelta))
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

        public static BindableProperty GaugeColorProperty = BindableProperty.Create(propertyName: nameof(GaugeWidth),
                                                                                    returnType: typeof(Color),
                                                                                    declaringType: typeof(CircularGauge),
                                                                                    defaultBindingMode: BindingMode.OneWay,
                                                                                    propertyChanged: OnGaugeColorPropertyChanged);

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

        #endregion

        public CircularGauge()
        {
            InitializeComponent();
        }
    }
}