using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Common.EqualityComparers
{
    public sealed class SubsetsEqualityComparer<T> : IEqualityComparer<ICollection<ISet<T>>>
    {
        public static IEqualityComparer<ICollection<ISet<T>>> Instance { get; } = new SubsetsEqualityComparer<T>();

        private SubsetsEqualityComparer()
        {
        }

        public bool Equals(ICollection<ISet<T>> x, ICollection<ISet<T>> y)
        {
            if (x == null || y == null)
            {
                return false;
            }

            return x.Count == y.Count && x.All(element => y.Contains(element, SetEqualityComparer<T>.Instance));
        }

        public int GetHashCode([DisallowNull] ICollection<ISet<T>> obj) => throw new NotImplementedException();
    }
}