using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Algorithms;
using EnsureThat;
using SudokuSolver.Core.Models;

[assembly: InternalsVisibleTo("SudokuSolver.Core.Tests")]
namespace SudokuSolver.Core
{
    internal class SudokuSolverService : ISudokuSolverService
    {
        private readonly IAlgorithmX _algorithmX;

        public SudokuSolverService(IAlgorithmX algorithmX)
        {
            Ensure.That(algorithmX, nameof(algorithmX)).IsNotNull();

            _algorithmX = algorithmX;
        }

        public ISet<Cell> Solve(int dimensionality, ISet<Cell> knownCells)
        {
            Ensure.That(dimensionality, nameof(dimensionality)).IsGt(0);
            Ensure.That(knownCells, nameof(knownCells)).IsNotNull();
            
            var cellsForCoverage = GetAllCells(dimensionality);
            var conditionsForCoverage = GetAllConditions(dimensionality);
            
            foreach (var cell in cellsForCoverage.Where(x => knownCells.Any(x.HasConflictWith)).ToList())
            {
                cellsForCoverage.Remove(cell);
            }
            
            foreach (var condition in knownCells.SelectMany(x => x.GetSatisfiedConditions()))
            {
                conditionsForCoverage.Remove(condition);
            }
            
            var availableConditions =
                new HashSet<ISet<Condition>>(cellsForCoverage.Select(x => x.GetSatisfiedConditions()));

            var exactCover = _algorithmX.GetExactCover(conditionsForCoverage, availableConditions);

            if (!exactCover.HasValue)
            {
                throw new ArgumentException("There is no one solution.");
            }
            
            return new HashSet<Cell>(exactCover.Value.Select(x => new Cell(x)).Concat(knownCells));
        }

        private static ISet<Cell> GetAllCells(int dimensionality)
        {
            var fieldSize = (int)Math.Pow(dimensionality, 2);
            var result = new HashSet<Cell>((int)Math.Pow(fieldSize, 3));

            for (int i = 0; i < fieldSize; ++i)
            {
                for (int j = 0; j < fieldSize; ++j)
                {
                    for (int k = 1; k <= fieldSize; ++k)
                    {
                        result.Add(new Cell(dimensionality, (i, j, k)));
                    }
                }
            }

            return result;
        }

        private static ISet<Condition> GetAllConditions(int dimensionality)
        {
            var fieldSize = (int)Math.Pow(dimensionality, 2);
            var result = new HashSet<Condition>(4 * fieldSize);

            for (int i = 0; i < 4; ++i)
            {
                for (int j = 0; j < fieldSize; ++j)
                {
                    for (int k = 0; k < fieldSize; ++k)
                    {
                        if ((ConditionType)i != ConditionType.Cell)
                        {
                            result.Add(new Condition((ConditionType)i, (j, k + 1)));
                        }
                        else
                        {
                            result.Add(new Condition((ConditionType)i, (j, k)));
                        }
                    }
                }
            }

            return result;
        }
    }
}