using System.Globalization;
using System.Windows.Data;
using GameTracker.Models;

namespace GameTracker.Converters
{
    public class GenreListToCommaConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IEnumerable<Genre> list)
                return string.Join(", ", list.Select(g => g.Name));
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((string)value)?.Split(',').Select(s => s.Trim()).ToList();
        }
    }
}