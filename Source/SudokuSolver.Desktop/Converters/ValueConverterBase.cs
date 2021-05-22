using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace SudokuSolver.Desktop.Converters
{
    public abstract class ValueConverterBase<TSelf> : MarkupExtension, IValueConverter where TSelf : ValueConverterBase<TSelf>, new()
    {
        private static readonly Lazy<TSelf> Instance = new(() => new TSelf());
        public override object ProvideValue(IServiceProvider serviceProvider) => Instance.Value;

        public virtual object Convert(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture) => throw new NotImplementedException();
        
        public virtual object ConvertBack(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture) => throw new NotImplementedException();
    }
}