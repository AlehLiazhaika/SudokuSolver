using System;
using System.Windows.Input;

namespace SudokuSolver.Desktop.Extensions
{
    public static class KeyExtension
    {
        public static int ToInt(this Key key) =>
            key switch
            {
                Key.D1 => 1,
                Key.D2 => 2,
                Key.D3 => 3,
                Key.D4 => 4,
                Key.D5 => 5,
                Key.D6 => 6,
                Key.D7 => 7,
                Key.D8 => 8,
                Key.D9 => 9,
                _ => throw new ArgumentOutOfRangeException(message: $"Cannot convert {key} to int", paramName: nameof(key)),
            };
    }
}