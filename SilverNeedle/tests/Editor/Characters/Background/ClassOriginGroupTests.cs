// //-----------------------------------------------------------------------
// // <copyright file="HomelandGatewayTests.cs" company="Short Leg Studio, LLC">
// //     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------
using NUnit.Framework;
using SilverNeedle;
using SilverNeedle.Characters.Background;
using System.Linq;
using SilverNeedle.Yaml;

namespace Tests.Characters.Background
{
    [TestFixture]
    public class ClassOriginGroupTests
    {
        [Test]
        public void LoadsUpClassOriginFromYamlFileWithExpectedAttributes()
        {
            var list = ClassOriginYamlFile.ParseYaml().Load<ClassOriginGroup>();
            var bardOriginTable = list.First(x => x.Name.EqualsIgnoreCase("bard")).Origins;
            var entry = bardOriginTable.All().First().Option;
            Assert.AreEqual("Celebrity", entry.Name);
            Assert.AreEqual(10, entry.Weighting);
            Assert.IsTrue(entry.Traits.Contains("Influence"));
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

