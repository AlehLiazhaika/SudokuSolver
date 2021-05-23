using System;
using System.Globalization;
using System.Windows;

namespace SudokuSolver.Desktop.Converters
{
    public class MarginConverter : ValueConverterBase<MarginConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                throw new ArgumentNullException();
            }

            return parameter switch
            {
                "Cell" => new Thickness(0, 0, IsEdgeIndex((int) value) ? 4 : 0, 0),
                "Row" => new Thickness(0, 0, 0, IsEdgeIndex((int)value) ? 4 : 0),
                _ => throw new ArgumentOutOfRangeException()
            };


            static bool IsEdgeIndex(int index) => index % 3 == 2;
        }
    }
}