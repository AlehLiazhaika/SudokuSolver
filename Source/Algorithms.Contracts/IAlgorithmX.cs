using System.Collections.Generic;

namespace Algorithms.Contracts
{
    public interface IAlgorithmX
    {
        /// <summary>
        /// Returns all sub-collections S* of S such that each element in X is contained in exactly one subset in S*.
        /// </summary>
        /// <typeparam name="T">A type of elements.</typeparam>
        /// <param name="setX">A set X.</param>
        /// <param name="collectionS">A collection S of subsets of a set X.</param>
        /// <returns>First sub-collection S* of S such that each element in X is contained in exactly one subset in S*</returns>
        ICollection<ISet<T>> Solve<T>(ISet<T> setX, ICollection<ISet<T>> collectionS);
    }
}