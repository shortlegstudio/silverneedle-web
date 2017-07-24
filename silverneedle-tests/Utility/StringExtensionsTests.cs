// Copyright (c) 2016 Trevor Redfern
//
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php

namespace Tests.Utility
{
    using Xunit;
    using SilverNeedle;

    public class StringExtensionsTests
    {
        [Fact]
        public void EqualsIgnoreCaseTests()
        {
            string foo = "Foo";
            Assert.True(foo.EqualsIgnoreCase("foo"));
            Assert.False(foo.EqualsIgnoreCase("fu"));
        }

        [Fact]
        public void DropEmptyItems()
        {
            var list = "foo,,".ParseList();
            Assert.Equal(1, list.Length);
        }
    }
}