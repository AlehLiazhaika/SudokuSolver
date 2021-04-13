using System.Collections.Generic;
using SudokuSolver.Core.Contracts.Models;

namespace SudokuSolver.Core.Contracts
{
    public interface ISudokuSolverService
    {
        ISet<Cell> Solve(int dimensionality, ISet<Cell> knownCells);
    }
}