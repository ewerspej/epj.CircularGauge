using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace epj.CircularGauge
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CircularGauge : ContentView
    {
        #region Properties
        public float StartAngle
        {
            get => GaugeCanvas.StartAngle;
            set => GaugeCanvas.StartAngle = value;
        }

        public float SweepAngle
        {
            get => GaugeCanvas.SweepAngle;
            set => GaugeCanvas.SweepAngle = value;
        }

        public float GaugeWidth
        {
            get => GaugeCanvas.GaugeWidth;
            set => GaugeCanvas.GaugeWidth = value;
        }

        public Color GaugeColor
        {
            get => GaugeCanvas.GaugeColor;
            set => GaugeCanvas.GaugeColor = value;
        }

        #endregion

        #region Bindable Properties

        public BindableProperty StartAngleProperty = BindableProperty.Create(propertyName: nameof(StartAngle),
                                                                             returnType: typeof(float),
                                                                             declaringType: typeof(CircularGauge),
                                                                             defaultBindingMode: BindingMode.OneWay,
                                                                             propertyChanged: OnStartAnglePropertyChanged);

        public BindableProperty SweepAngleProperty = BindableProperty.Create(propertyName: nameof(SweepAngle),
                                                                             returnType: typeof(float),
                                                                             declaringType: typeof(CircularGauge),
                                                                             defaultBindingMode: BindingMode.OneWay,
                                                                             propertyChanged: OnSweepAnglePropertyChanged);

        public BindableProperty GaugeWidthProperty = BindableProperty.Create(propertyName: nameof(GaugeWidth),
                                                                             returnType: typeof(float),
                                                                             declaringType: typeof(CircularGauge),
                                                                             defaultBindingMode: BindingMode.OneWay,
                                                                             propertyChanged: OnGaugeWidthPropertyChanged);

        public BindableProperty GaugeColorProperty = BindableProperty.Create(propertyName: nameof(GaugeWidth),
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