using System;
using System.Globalization;
using Xamarin.Forms;

namespace Simple.Xamarin.Framework
{
    public class NegateBoolValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || !(value is bool boolValue))
                return null;

            return !boolValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
