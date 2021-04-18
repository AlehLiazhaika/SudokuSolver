using System;
using System.Collections.Generic;
using Algorithms.Tests.xUnit.EqualityComparers;
using SuccincT.Options;
using Xunit;

namespace Algorithms.Tests.xUnit
{
    public class AlgorithmXTests
    {
        private readonly AlgorithmX _algorithmX;

        public AlgorithmXTests() => _algorithmX = new AlgorithmX();

        [Fact]
        public void Solve_SubsetsWithNoExactCover_None()
        {
            // Arrange
            var setX = new HashSet<int>(new[] { 1, 2, 3, 4 });
            var subsetsX = new List<ISet<int>>(new[]
            {
                new HashSet<int>(new []{1}),
                new HashSet<int>(new []{1, 2}),
                new HashSet<int>(new []{1, 2}),
                new HashSet<int>(new []{1, 2, 3})
            });

            var expected = Option<ICollection<ISet<int>>>.None();

            // Act
            var actual = _algorithmX.GetExactCover(setX, subsetsX);

            // Assert
            Assert.Equal(expected, actual, ExactCoverEqualityComparer<int>.Instance);
        }

        [Fact]
        public void Solve_SubsetsWithSingleExactCover_ExactCover()
        {
            // Arrange
            var setX = new HashSet<int>(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 });
            var subsetsX = new List<ISet<int>>(new[]
            {
                new HashSet<int>(new []{1, 4, 8, 11}),
                new HashSet<int>(new []{4, 7, 9}),
                new HashSet<int>(new []{1, 10}),
                new HashSet<int>(new []{1, 3, 4, 5, 7, 8, 9}),
                new HashSet<int>(new []{2, 5}),
                new HashSet<int>(new []{2, 6, 10, 11}),
                new HashSet<int>(new []{3, 5})
            });

            var expected = new List<ISet<int>>(new[]
            {
                new HashSet<int>(new []{1, 3, 4, 5, 7, 8, 9}),
                new HashSet<int>(new []{2, 6, 10, 11})
            });

            // Act
            var actual = _algorithmX.GetExactCover(setX, subsetsX);

            // Assert
            Assert.Equal(expected, actual, ExactCoverEqualityComparer<int>.Instance);
        }

        [Fact]
        public void Solve_SubsetsWithMultipleExactCover_FirstExactCover()
        {
            // Arrange
            var setX = new HashSet<int>(new[] { 1, 2, 3, 4, 5, 6 });
            var subsetsX = new List<ISet<int>>(new[]
            {
                new HashSet<int>(new []{1, 3, 5}),
                new HashSet<int>(new []{2, 4, 6}),
                new HashSet<int>(new []{1, 2, 3}),
                new HashSet<int>(new []{4, 5, 6}),
                new HashSet<int>(new []{1, 2}),
                new HashSet<int>(new []{3, 4})
            });

            var expected = new List<ISet<int>>(new[]
            {
                new HashSet<int>(new []{1, 3, 5}),
                new HashSet<int>(new []{2, 4, 6}),
            });

            // Act
            var actual = _algorithmX.GetExactCover(setX, subsetsX);

            // Assert
            Assert.Equal(expected, actual, ExactCoverEqualityComparer<int>.Instance);
        }

        [Fact]
        public void Solve_SubsetsWithStringElements_ExactCover()
        {
            // Arrange
            var setX = new HashSet<string>(new[]
            {
                "Bear",
                "Elephant",
                "Kangaroo",
                "Koala",
                "Lion",
                "Monkey",
                "Penguin",
                "Tiger"
            });

            var subsetsX = new List<ISet<string>>(new[]
            {
                new HashSet<string>(new []{"Bear", "Elephant", "Koala"}),
                new HashSet<string>(new []{"Kangaroo", "Lion", "Penguin"}),
                new HashSet<string>(new []{"Kangaroo", "Tiger", "Bear", "Elephant"}),
                new HashSet<string>(new []{"Monkey", "Tiger"}),
                new HashSet<string>(new []{"Kangaroo"})
            });

            var expected = new List<ISet<string>>(new[]
            {
                new HashSet<string>(new []{"Bear", "Elephant", "Koala"}),
                new HashSet<string>(new []{"Kangaroo", "Lion", "Penguin"}),
                new HashSet<string>(new []{"Monkey", "Tiger"})
            });

            // Act
            var actual = _algorithmX.GetExactCover(setX, subsetsX);

            // Assert
            Assert.Equal(expected, actual, ExactCoverEqualityComparer<string>.Instance);
        }

        [Fact]
        public void Solve_SubsetsWithInvalidElements_ThrowsArgumentException()
        {
            // Arrange
            var setX = new HashSet<int>(new[] { 1, 2, 3, 4, 5 });
            var subsetsX = new List<ISet<int>>(new[]
            {
                new HashSet<int>(new []{1, 3, 5}),
                new HashSet<int>(new []{2, 4, 6}),
                new HashSet<int>(new []{1, 2, 3}),
                new HashSet<int>(new []{4, 5, 6}),
                new HashSet<int>(new []{1, 2}),
                new HashSet<int>(new []{3, 4})
            });

            var expected = new ArgumentException(message: "One or more of subsets elements do not consist in the basic set X");

            // Act & Assert
            var actual = Assert.Throws<ArgumentException>(() => _algorithmX.GetExactCover(setX, subsetsX));
            Assert.Equal(expected.Message, actual.Message);
        }
    }
}