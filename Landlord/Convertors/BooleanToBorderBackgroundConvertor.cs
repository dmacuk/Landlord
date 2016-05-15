using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Landlord.Convertors
{
    [ValueConversion(typeof(bool), typeof(Color))]
    public class BooleanToBorderBackgroundConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var hidden = (bool)value;
            return hidden ? Brushes.Red : null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}