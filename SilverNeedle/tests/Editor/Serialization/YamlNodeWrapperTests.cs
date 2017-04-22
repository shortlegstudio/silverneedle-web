// Copyright (c) 2017 Trevor Redfern
//
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Serialization
{
    using System.Linq;
    using NUnit.Framework;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    [TestFixture]
    public class YamlNodeWrapperTests
    {
        [Test]
        public void ProvidesAListOfKeysForNode()
        {
            string demo = @"---
node1: value
node2: value
node3: value
node4: value
...";
            IObjectStore y = demo.ParseYaml();
            Assert.AreEqual(4, y.Keys.Count());
            Assert.AreEqual("node1", y.Keys.ElementAt(0));
            Assert.AreEqual("node2", y.Keys.ElementAt(1));
            Assert.AreEqual("node3", y.Keys.ElementAt(2));
            Assert.AreEqual("node4", y.Keys.ElementAt(3));
        }

        [Test]
        public void CanGetBooleanValues()
        {
            string test = "boolean: true";
            IObjectStore n = test.ParseYaml();
            Assert.IsTrue(n.GetBool("boolean"));

            string falsies = "boolean: false";
            n = falsies.ParseYaml();
            Assert.IsFalse(n.GetBool("boolean"));
        }

    }
}