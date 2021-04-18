using System.Collections.Generic;
using SuccincT.Options;

namespace Algorithms.Contracts
{
    public interface IAlgorithmX
    {
        /// <summary>
        /// Returns an exact cover of set X.
        /// </summary>
        /// <typeparam name="T">A type of elements.</typeparam>
        /// <param name="setX">A set X.</param>
        /// <param name="collectionS">A collection S of subsets of a set X.</param>
        /// <returns>Sub-collection S* of S such that each element in X is contained in exactly one subset in S*</returns>
        Option<ICollection<ISet<T>>> GetExactCover<T>(ISet<T> setX, ICollection<ISet<T>> collectionS);
    }
}