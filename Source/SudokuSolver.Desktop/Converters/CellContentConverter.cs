using System;
using System.Globalization;

namespace SudokuSolver.Desktop.Converters
{
    public class CellContentConverter : ValueConverterBase<CellContentConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                throw new ArgumentNullException();
            }

            return (int) value == 0 ? string.Empty : ((int) value).ToString();
        }
    }
}