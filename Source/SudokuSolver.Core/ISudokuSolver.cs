using System.Collections.Generic;
using SudokuSolver.Core.Models;

namespace SudokuSolver.Core
{
    public interface ISudokuSolver
    {
        ISet<Cell> Solve(ISet<Cell> knownCells);
    }
}