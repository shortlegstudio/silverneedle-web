// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests
{
    using NUnit.Framework;
    using SilverNeedle;

    [TestFixture]
    public class DynamicPropertyTests
    {
        [Test]
        public void UsingAPropertyNameItInstantiatesTheClass()
        {
            var propName = "(Tests.DynamicPropertyTestDummy)";
            var dynProp = new DynamicProperty(propName);
            Assert.That(dynProp.Processor, Is.InstanceOf(typeof(DynamicPropertyTestDummy)));
        }

        [Test]
        public void YouCanCallProcessOnItForAnAnswer()
        {
            var propName = "(Tests.DynamicPropertyTestDummy)";
            var dynProp = new DynamicProperty(propName);
            var result = dynProp.GetString();
            Assert.That(result, Is.EqualTo("dummy-prop"));
        }

        [Test]
        public void IfMissingCurlyBracesJustReturnTheValuePassedIn()
        {
            var propName = "foobar";
            var dynProp = new DynamicProperty(propName);
            var result = dynProp.GetString();
            Assert.That(result, Is.EqualTo("foobar"));
        }
    }

    public class DynamicPropertyTestDummy : IDynamicPropertyEvaluator
    {
        public string GetString()
        {
            return "dummy-prop";
        }
    }
}