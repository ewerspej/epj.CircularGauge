using System;
using System.Globalization;
using Xamarin.Forms;

namespace CircularGaugeSample.Converters
{
    public class StringToFloatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (float.TryParse((string)value, NumberStyles.AllowDecimalPoint, null, out var returnValue))
            {
                return returnValue;
            }

            throw new ArgumentException("Cannot parse string to float");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
