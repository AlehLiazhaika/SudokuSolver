using EnsureThat;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace SudokuSolver.Desktop.ViewModels
{
    public class CellViewModel : ReactiveObject
    {
        public int Row { get; }
        public int Column { get; }
        public int Box { get; }

        [Reactive] public int Value { get; set; }
        [Reactive] public bool IsSelected { get; set; }

        public CellViewModel(int boxDimensionality, int row, int column, int value)
        {
            Ensure.That(boxDimensionality, nameof(boxDimensionality)).IsGt(0);
            Ensure.That(row, nameof(row)).IsGte(0);
            Ensure.That(column, nameof(column)).IsGte(0);
            Ensure.That(value, nameof(value)).IsGte(0);

            Row = row;
            Column = column;
            Value = value;
            Box = boxDimensionality * (row / boxDimensionality) + column / boxDimensionality;
        }
    }
}