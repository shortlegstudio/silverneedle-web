// //-----------------------------------------------------------------------
// // <copyright file="DrawbackYamlGatewayTests.cs" company="Short Leg Studio, LLC">
// //     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------
using NUnit.Framework;
using SilverNeedle;
using SilverNeedle.Characters.Background;
using System.Linq;
using SilverNeedle.Yaml;

namespace Characters
{
    [TestFixture]
    public class DrawbackYamlGatewayTests
    {
        [Test]
        public void MakesAllDrawbacksAvailableUponRequest()
        {
            var gateway = new DrawbackYamlGateway(DrawbackYamlFile.ParseYaml());
            var drawbacks = gateway.GetDrawbacks().All();
            Assert.Greater(drawbacks.Count(), 0);
            Assert.IsTrue(drawbacks.Any(x => x.Option.Name == "The Future"));
        }

        private const string DrawbackYamlFile = @"--- 
- drawback:
  name: Social Acceptance
  weight: 5
  traits: Dependent
- drawback:
  name: The Future
  weight: 5
  traits: Meticulous
- drawback:
  name: The Past
  weight: 5
  traits: Sentimental
";
    }
}

