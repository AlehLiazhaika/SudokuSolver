using System;
using System.Collections.Generic;
using System.Linq;
using EnsureThat;
using SudokuSolver.Core.Models;

namespace SudokuSolver.Core
{
    public class SudokuSolver : ISudokuSolver // TODO internal
    {
        private readonly AlgorithmX.AlgorithmX _algorithmX;
        private readonly int _dimensionality;

        public SudokuSolver(int dimensionality, AlgorithmX.AlgorithmX algorithmX) // Replace with interfaces (Ioc, DI) 
        {
            Ensure.That(dimensionality, nameof(dimensionality)).IsGt(0);
            Ensure.That(algorithmX, nameof(algorithmX)).IsNotNull();

            _algorithmX = algorithmX;
            _dimensionality = dimensionality;
        }

        public ISet<Cell> Solve(ISet<Cell> knownCells)
        {
            var N = (int)Math.Pow(_dimensionality, 2);
            var allCells = GetAllCells();
            var allConditions = GetAllPossibleConditions();

            // Remove conflict with known

            foreach (var cell in allCells.Where(x => knownCells.Any(x.HasConflictWith)).ToList())
            {
                allCells.Remove(cell);
            }

            // Remove completed conditions

            foreach (var cell in knownCells)
            {
                foreach (var condition in cell.GetSatisfiedConditions())
                {
                    allConditions.Remove(condition);
                }
            }

            // Solve exact cover problem

            var conditions = new HashSet<ISet<Condition>>(allCells.Select(x => x.GetSatisfiedConditions()));

            var result = _algorithmX.Solve(allConditions, conditions).Select(x => new Cell(x));


            // Concat known cells and result
            

            return new HashSet<Cell>(result.Concat(knownCells));


            ISet<Cell> GetAllCells()
            {
                var result = new HashSet<Cell>((int)Math.Pow(N, 3));

                for (int i = 0; i < N; ++i)
                {
                    for (int j = 0; j < N; ++j)
                    {
                        for (int k = 1; k <= N; ++k)
                        {
                            result.Add(new Cell(_dimensionality, (i, j, k)));
                        }
                    }
                }

                return result;
            }

            ISet<Condition> GetAllPossibleConditions()
            {
                var result = new HashSet<Condition>(4 * N);

                for (int i = 0; i < 4; ++i)
                {
                    for (int j = 0; j < N; ++j)
                    {
                        for (int k = 0; k < N; ++k)
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
}