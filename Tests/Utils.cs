using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace NoodleApi.Tests.Util
{
    /* 
        The CollectionEquivalenceComparer class is a utility class used
        to perform equivalence comparisons between two generic collections.
    */
    /// <summary>
    ///    The <c>CourseControllerTests</c> class contains all tests for the 
    ///    <c>CourseController</c> API class.
    /// </summary>
    /// <remarks>
    /// <para>
    /// There is at least one test method for each controller method.
    /// </para>
    /// <para>
    /// A few private utility methods are used for convenience.
    /// </para>
    /// </remarks>
    public class CollectionEquivalenceComparer<T> : IEqualityComparer<IEnumerable<T>>
        where T : IEquatable<T>
    {
        // Compares two IEnumerable instances
        /// <summary>
        ///     Compares two <c>IEnumerable</c> instances <paramref name="x"/> 
        ///     and <paramref name="y"/> for equivalence.
        /// </summary>
        /// <returns>
        ///     True, if collections are equivalent, comparing every element.
        ///     False, if collections are not equivalent.
        /// </returns>
        /// <param name="x">The first collection</param>
        /// <param name="y">The second collection</param>
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
        /// <summary>
        ///     Returns the hash code produced by the current instance 
        ///     and <paramref name="obj"/>.
        /// </summary>
        /// <returns>
        ///     An int, the collection's hash code.
        /// </returns>
        /// <exception cref="System.NotImplementedException">
        ///     Thrown when an invoked method is not implemented yet.
        /// </exception>
        /// <param name="obj">The other collection</param>
        public int GetHashCode(IEnumerable<T> obj)
        {
            throw new NotImplementedException();
        }
    }
}