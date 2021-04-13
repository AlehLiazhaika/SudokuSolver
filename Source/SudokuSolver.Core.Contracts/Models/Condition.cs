using System;
using EnsureThat;

namespace SudokuSolver.Core.Contracts.Models
{
    public enum ConditionType
    {
        Row,
        Column,
        Cell,
        Box
    }

    public class Condition
    {
        public ConditionType Type { get; }
        public (int, int) Value { get; }

        public Condition(ConditionType type, (int, int) value)
        {
            Ensure.That(value.Item1).IsGte(0);
            Ensure.That(value.Item2).IsGte(0);

            Type = type;
            Value = value;
        }

        public override bool Equals(object obj) => Equals(obj as Condition);

        public bool Equals(Condition condition) =>
            condition != null &&
            Type == condition.Type &&
            Value == condition.Value;

        public override int GetHashCode() => HashCode.Combine(Type, Value);
    }
}