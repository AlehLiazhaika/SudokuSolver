using System.Collections.Generic;
using SudokuSolver.Core.Models;

namespace SudokuSolver.Core
{
    public interface ISudokuSolverService
    {
        ISet<Cell> Solve(int dimensionality, ISet<Cell> knownCells);
    }
}