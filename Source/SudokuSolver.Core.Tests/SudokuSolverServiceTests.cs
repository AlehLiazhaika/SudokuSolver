using System;
using System.Collections.Generic;
using Algorithms;
using Common.EqualityComparers;
using NSubstitute;
using SuccincT.Options;
using SudokuSolver.Core.Models;
using Xunit;

namespace SudokuSolver.Core.Tests
{
    public class SudokuSolverServiceTests
    {
        /// <summary>
        /// Input:
        ///     1 2 3 4
        ///     3 4 1 2
        ///     _ 1 2 3
        ///     _ 3 _ 1
        /// Expected:
        ///     1 2 3 4
        ///     3 4 1 2
        ///     4 1 2 3
        ///     2 3 4 1
        /// </summary>
        [Fact]
        public void Solve_SimpleSudokuProblem_CompletedSudokuProblem()
        {
            // Arrange
            var dimensionality = 2;
            var knownCells = new HashSet<Cell>(new[]
            {
                new Cell(dimensionality, row: 0, column: 0, value: 1),
                new Cell(dimensionality, row: 0, column: 1, value: 2),
                new Cell(dimensionality, row: 0, column: 2, value: 3),
                new Cell(dimensionality, row: 0, column: 3, value: 4),
                new Cell(dimensionality, row: 1, column: 0, value: 3),
                new Cell(dimensionality, row: 1, column: 1, value: 4),
                new Cell(dimensionality, row: 1, column: 2, value: 1),
                new Cell(dimensionality, row: 1, column: 3, value: 2),
                new Cell(dimensionality, row: 2, column: 1, value: 1),
                new Cell(dimensionality, row: 2, column: 2, value: 2),
                new Cell(dimensionality, row: 2, column: 3, value: 3),
                new Cell(dimensionality, row: 3, column: 1, value: 3),
                new Cell(dimensionality, row: 3, column: 3, value: 1)
            });

            var setX = new HashSet<Condition>(new[]
            {
                new Condition(ConditionType.Row, (2, 4)),
                new Condition(ConditionType.Row, (3, 2)),
                new Condition(ConditionType.Row, (3, 4)),
                new Condition(ConditionType.Column, (0, 2)),
                new Condition(ConditionType.Column, (0, 4)),
                new Condition(ConditionType.Column, (2, 4)),
                new Condition(ConditionType.Cell, (2, 0)),
                new Condition(ConditionType.Cell, (3, 0)),
                new Condition(ConditionType.Cell, (3, 2)),
                new Condition(ConditionType.Box, (2, 2)),
                new Condition(ConditionType.Box, (2, 4)),
                new Condition(ConditionType.Box, (3, 4))
            });

            var subsetsX = new List<ISet<Condition>>(new[]
            {
                new HashSet<Condition>(new Cell(dimensionality, row: 2, column: 0, value: 4).GetSatisfiedConditions()),
                new HashSet<Condition>(new Cell(dimensionality, row: 3, column: 0, value: 2).GetSatisfiedConditions()),
                new HashSet<Condition>(new Cell(dimensionality, row: 3, column: 0, value: 4).GetSatisfiedConditions()),
                new HashSet<Condition>(new Cell(dimensionality, row: 3, column: 2, value: 4).GetSatisfiedConditions())
            });

            var exactCover = new HashSet<ISet<Condition>>(new[]
            {
                new HashSet<Condition>(new Cell(dimensionality, row: 2, column: 0, value: 4).GetSatisfiedConditions()),
                new HashSet<Condition>(new Cell(dimensionality, row: 3, column: 0, value: 2).GetSatisfiedConditions()),
                new HashSet<Condition>(new Cell(dimensionality, row: 3, column: 2, value: 4).GetSatisfiedConditions())
            });

            var expected = new HashSet<Cell>(new[]
            {
                new Cell(dimensionality, row: 0, column: 0, value: 1),
                new Cell(dimensionality, row: 0, column: 1, value: 2),
                new Cell(dimensionality, row: 0, column: 2, value: 3),
                new Cell(dimensionality, row: 0, column: 3, value: 4),
                new Cell(dimensionality, row: 1, column: 0, value: 3),
                new Cell(dimensionality, row: 1, column: 1, value: 4),
                new Cell(dimensionality, row: 1, column: 2, value: 1),
                new Cell(dimensionality, row: 1, column: 3, value: 2),
                new Cell(dimensionality, row: 2, column: 0, value: 4),
                new Cell(dimensionality, row: 2, column: 1, value: 1),
                new Cell(dimensionality, row: 2, column: 2, value: 2),
                new Cell(dimensionality, row: 2, column: 3, value: 3),
                new Cell(dimensionality, row: 3, column: 0, value: 2),
                new Cell(dimensionality, row: 3, column: 1, value: 3),
                new Cell(dimensionality, row: 3, column: 3, value: 1),
                new Cell(dimensionality, row: 3, column: 2, value: 4)
            });

            var algorithmX = Substitute.For<IAlgorithmX>();
            algorithmX
                .GetExactCover(
                    Arg.Is<ISet<Condition>>(x => SetEqualityComparer<Condition>.Instance.Equals(x, setX)),
                    Arg.Is<ICollection<ISet<Condition>>>(x =>
                        SubsetsEqualityComparer<Condition>.Instance.Equals(x, subsetsX)))
                .Returns(Option<ICollection<ISet<Condition>>>.Some(exactCover));

            var sudokuSolverService = new SudokuSolverService(algorithmX);

            // Act
            var actual = sudokuSolverService.Solve(dimensionality, knownCells);

            // Assert
            Assert.Equal(expected, actual, SetEqualityComparer<Cell>.Instance);
        }

        /// <summary>
        /// Input:
        ///     1 _ _ _
        ///     _ 1 _ _
        ///     _ _ _ _
        ///     _ _ _ _
        /// </summary>
        [Fact]
        public void Solve_IncompatibleCells_ThrowsArgumentException()
        {
            // Arrange
            var dimensionality = 2;
            var knownCells = new HashSet<Cell>(new[]
            {
                new Cell(dimensionality, row: 0, column: 0, value: 1),
                new Cell(dimensionality, row: 1, column: 1, value: 1)
            });

            var expected = new ArgumentException("There is no one solution.");

            var algorithmX = Substitute.For<IAlgorithmX>();
            algorithmX
                .GetExactCover(
                    Arg.Any<ISet<Condition>>(), 
                    Arg.Any<ICollection<ISet<Condition>>>())
                .Returns(Option<ICollection<ISet<Condition>>>.None());

            var sudokuSolverService = new SudokuSolverService(algorithmX);

            // Act & Assert
            var actual = Assert.Throws<ArgumentException>(() => sudokuSolverService.Solve(dimensionality, knownCells));
            Assert.Equal(expected.Message, actual.Message);
        }
    }
}