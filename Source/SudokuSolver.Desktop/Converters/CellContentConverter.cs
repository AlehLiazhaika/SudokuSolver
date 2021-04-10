using System;
using System.Globalization;
using System.Windows.Data;

namespace SudokuSolver.Desktop.Converters
{
    public class CellContentConverter : IValueConverter // TODO Use MarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                throw new ArgumentNullException();
            }

            return (int) value == 0 ? string.Empty : ((int) value).ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new NotImplementedException();
    }
}