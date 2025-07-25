using System.Globalization;
using System.Windows.Data;

namespace GameTracker.Converters
{
    public class StringToIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            int.Parse(value.ToString());

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            value.ToString();
    }
}