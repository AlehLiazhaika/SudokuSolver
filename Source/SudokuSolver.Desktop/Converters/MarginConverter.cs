using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SudokuSolver.Desktop.Converters
{
    public class MarginConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
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


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new NotImplementedException();
    }
}