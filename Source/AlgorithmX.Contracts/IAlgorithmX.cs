using System.Collections.Generic;

namespace AlgorithmX.Contracts
{
    public interface IAlgorithmX
    {
        /// <summary>
        /// Returns all subcollections S* of S such that each element in X is contained in exactly one subset in S*.
        /// </summary>
        /// <typeparam name="T">A type of elements.</typeparam>
        /// <param name="setX">A set X.</param>
        /// <param name="collectionS">A collection S of subsets of a set X.</param>
        /// <returns>First subcollection S* of S such that each element in X is contained in exactly one subset in S*</returns>
        ICollection<ISet<T>> Solve<T>(ISet<T> setX, ICollection<ISet<T>> collectionS);
    }
}