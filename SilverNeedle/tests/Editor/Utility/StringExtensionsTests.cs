// Copyright (c) 2016 Trevor Redfern
//
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php

namespace Tests.Utility
{
    using NUnit.Framework;
    using SilverNeedle;

    [TestFixture]
    public class StringExtensionsTests
    {
        [Test]
        public void EqualsIgnoreCaseTests()
        {
            string foo = "Foo";
            Assert.IsTrue(foo.EqualsIgnoreCase("foo"));
            Assert.IsFalse(foo.EqualsIgnoreCase("fu"));
        }

        [Test]
        public void DropEmptyItems()
        {
            var list = "foo,,".ParseList();
            Assert.AreEqual(1, list.Length);
        }
    }
}