using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using SuccincT.Options;

namespace Common.EqualityComparers
{
    public sealed class ExactCoverEqualityComparer<T> : IEqualityComparer<Option<ICollection<ISet<T>>>>
    {
        public static IEqualityComparer<Option<ICollection<ISet<T>>>> Instance { get; } = new ExactCoverEqualityComparer<T>();

        private ExactCoverEqualityComparer()
        {
        }

        public bool Equals(Option<ICollection<ISet<T>>> x, Option<ICollection<ISet<T>>> y)
        {
            if (!x.HasValue && !y.HasValue)
            {
                return true;
            }

            if (x.HasValue && y.HasValue)
            {
                return x.Value.Count == y.Value.Count 
                       && x.Value.All(element => y.Value.Contains(element, SetEqualityComparer<T>.Instance));
            }

            return false;
        }

        public int GetHashCode([DisallowNull] Option<ICollection<ISet<T>>> obj) => throw new NotImplementedException();
    }
}