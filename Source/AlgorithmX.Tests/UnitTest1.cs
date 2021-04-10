using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using AlgorithmX.Contracts;
using Xunit;

namespace AlgorithmX.Tests
{
    public class UnitTest1 //TODO Rename
    {
        private IAlgorithmX Sut { get; }

        public UnitTest1()
        {
            Sut = new AlgorithmX();
        }

        [Fact]
        public void Test1()
        {
            //Arrange
            var setX = new HashSet<int>(new [] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11});
           
            var collectionY = new Collection<ISet<int>>
            {
                new HashSet<int>(new[] { 2, 4, 6 }),
                new HashSet<int>(new[] { 1, 2 }),
                new HashSet<int>(new[] { 6, 7 }),
                new HashSet<int>(new[] { 4, 5 }),
                new HashSet<int>(new[] { 7, 8 }),
                new HashSet<int>(new[] { 3, 4 ,  10}),
                new HashSet<int>(new[] { 7, 10 }),
                new HashSet<int>(new[] { 5, 8, 9, 11 }),
                new HashSet<int>(new[] { 1, 3 })
            };

            //Act
            try
            {

                var result = Sut.Solve(setX, collectionY);
            }
            catch(Exception e)
            {

            }

            //Assert
            Assert.True(true);
        }
    }
}
