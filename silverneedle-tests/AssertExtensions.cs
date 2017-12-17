// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests
{
    using Xunit;
    using System.Collections.Generic;
    using System.Linq;

    public static class AssertExtensions
    {
        public static void EquivalentLists<T>(IEnumerable<T> expected, IEnumerable<T> actual)
        {
            Assert.Equal(expected.OrderBy(x => x), actual.OrderBy(y => y));
        }

        public static void Contains<T>(T expected, IEnumerable<T> collection)
        {
            Assert.True(collection.Contains(expected));
        }

        public static void ListIsUnique<T>(IEnumerable<T> list)
        {
            var uniqueSet = new HashSet<T>(list);

            Assert.Equal(
                uniqueSet.Count(),
                list.Count()
            );
        }
    }
}