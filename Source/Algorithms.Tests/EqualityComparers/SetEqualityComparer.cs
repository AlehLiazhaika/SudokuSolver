using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Algorithms.Tests.xUnit.EqualityComparers
{
    internal sealed class SetEqualityComparer<T> : IEqualityComparer<ISet<T>>
    {
        public static IEqualityComparer<ISet<T>> Instance { get; } = new SetEqualityComparer<T>();

        private SetEqualityComparer()
        {
        }

        public bool Equals(ISet<T> x, ISet<T> y)
        {
            if (x == null || y == null)
            {
                return false;
            }

            return x.Count == y.Count && x.All(y.Contains);
        }

        public int GetHashCode([DisallowNull] ISet<T> obj) => throw new NotImplementedException();
    }
}