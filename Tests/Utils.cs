using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace NoodleApi.Tests.Util
{
    public class CollectionEquivalenceComparer<T> : IEqualityComparer<IEnumerable<T>>
        where T : IEquatable<T>
    {
        public bool Equals(IEnumerable<T> x, IEnumerable<T> y)
        {
            List<T> leftList = new List<T>(x);
            List<T> rightList = new List<T>(y);
            // leftList.Sort();
            // rightList.Sort();

            IEnumerator<T> enumX = leftList.GetEnumerator();
            IEnumerator<T> enumY = rightList.GetEnumerator();

            while (true)
            {
                bool hasNextX = enumX.MoveNext();
                bool hasNextY = enumY.MoveNext();

                if (!hasNextX || !hasNextY)
                    return (hasNextX == hasNextY);

                if (!enumX.Current.Equals(enumY.Current))
                    return false;
            }
        }

        public int GetHashCode(IEnumerable<T> obj)
        {
            throw new NotImplementedException();
        }
    }
}