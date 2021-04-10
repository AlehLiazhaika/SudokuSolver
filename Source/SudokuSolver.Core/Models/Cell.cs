using System.Collections.Generic;
using System.Linq;
using EnsureThat;

namespace SudokuSolver.Core.Models
{
    public class Cell
    {
        public int Row { get; }
        public int Column { get; }
        public int Value { get; }
        public int Box { get; }

        public Cell(int boxDimensionality, int row, int column, int value)
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

        public Cell(int boxDimensionality, (int row, int column, int value) cell)
            : this(boxDimensionality, cell.row, cell.column, cell.value)
        {
        }

        internal Cell(ISet<Condition> conditions)
        {
            Ensure.That(conditions, nameof(conditions)).IsNotNull();
            Ensure.Collection.SizeIs(conditions, 4);

            Row = conditions.First(x => x.Type == ConditionType.Row).Value.Item1;
            Column = conditions.First(x => x.Type == ConditionType.Column).Value.Item1;
            Value = conditions.First(x => x.Type == ConditionType.Box).Value.Item2;
            Box = conditions.First(x => x.Type == ConditionType.Box).Value.Item1;
        }

        public bool HasConflictWith(Cell cell) => GetSatisfiedConditions().Intersect(cell.GetSatisfiedConditions()).Any();

        internal ISet<Condition> GetSatisfiedConditions() =>
            new HashSet<Condition>(
                new Condition[]
                {
                    new Condition(ConditionType.Row, (Row, Value)),
                    new Condition(ConditionType.Column, (Column, Value)),
                    new Condition(ConditionType.Cell, (Row, Column)),
                    new Condition(ConditionType.Box, (Box, Value))
                });
    }
}