using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace NoodleApi.Tests.Util
{
    /* 
        The CollectionEquivalenceComparer class is a utility class used
        to perform equivalence comparisons between two generic collections.
    */
    /// <include file='docs/tests_util_doc.xml' path='/doc/members/member[@name="T:NoodleApi.Tests.Util.CollectionEquivalenceComparer"]/*'/>
    public class CollectionEquivalenceComparer<T> : IEqualityComparer<IEnumerable<T>>
        where T : IEquatable<T>
    {
        // Compares two IEnumerable instances
        /// <include file='docs/tests_util_doc.xml' path='/doc/members/member[@name="M:NoodleApi.Tests.Util.CollectionEquivalenceComparer.Equals(System.Collections.Generic.IEnumerable,System.Collections.Generic.IEnumerable)"]/*'/>
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

        // Produces the hash code of the current collection and a second collection
        /// <include file='docs/tests_util_doc.xml' path='/doc/members/member[@name="M:NoodleApi.Tests.Util.CollectionEquivalenceComparer.GetHashCode(System.Collections.Generic.IEnumerable)"]/*'/>
        public int GetHashCode(IEnumerable<T> obj)
        {
            throw new NotImplementedException();
        }
    }
}