// //-----------------------------------------------------------------------
// // <copyright file="HomelandYamlGatewayTests.cs" company="Short Leg Studio, LLC">
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
    public class ClassOriginYamlGatewayTests
    {
        [Test]
        public void LoadsUpClassOriginFromYamlFileWithExpectedAttributes()
        {
            var gateway = new ClassOriginYamlGateway(ClassOriginYamlFile.ParseYaml());
            var bardOriginTable = gateway.GetClassOriginOptions("bard");
            var entry = bardOriginTable.All().First().Option;
            Assert.AreEqual("Celebrity", entry.Name);
            Assert.AreEqual(10, entry.Weighting);
            Assert.IsTrue(entry.Traits.Contains("Influence"));
        }

        [Test]
        public void CaseInsensitiveSearchesForClassOrigins()
        {
            var gateway = new ClassOriginYamlGateway(ClassOriginYamlFile.ParseYaml());
            Assert.Greater(gateway.GetClassOriginOptions("Barbarian").All().Count(), 0);
            Assert.Greater(gateway.GetClassOriginOptions("BARD").All().Count(), 0);
        }

        private const string ClassOriginYamlFile = @"--- 
- background:
  class: barbarian
  table:
    - name: Vengeance
      weight: 10
      traits: Axe to Grind
      storylines: Foeslayer, Vengeance
    - name: Champion of a God
      weight: 10
      traits: Inspired
      storylines: Champion
- background:
  class: bard
  table:
    - name: Celebrity
      weight: 10
      traits: Charming, Influence
    - name: Cultural Mandate
      weight: 10
      traits: Fast Talker
";
    }
}

