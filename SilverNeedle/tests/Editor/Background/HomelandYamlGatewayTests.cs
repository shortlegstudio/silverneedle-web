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
    public class HomelandYamlGatewayTests
    {

        [Test]
        public void LoadsUpHomelandFromYamlFileWithExpectedAttributes()
        {
            var gateway = new HomelandYamlGateway(HomelandYamlFile.ParseYaml());
            var dwarfTable = gateway.GetRacialOptions("dwarf");
            Assert.AreEqual(3, dwarfTable.All().Count());
            var mountain = dwarfTable.All().First().Option;
            Assert.AreEqual("Mountain", mountain.Location);
            Assert.AreEqual(40, mountain.Weighting);
            Assert.IsTrue(mountain.Traits.Contains("Miner"));
            Assert.IsTrue(mountain.Traits.Contains("Climber"));
        }

        [Test]
        public void IsCaseInsensitiveOnRaceNames()
        {
            var gateway = new HomelandYamlGateway(HomelandYamlFile.ParseYaml());
            var dwarfTable = gateway.GetRacialOptions("Human");
            Assert.AreEqual(2, dwarfTable.All().Count());
        }

        private const string HomelandYamlFile = @"--- 
- homeland: 
  race: dwarf
  table:
    - location: Mountain
      weight: 40
      traits: Miner, Climber
    - location: Underground
      weight: 30
      traits: Miner, Adventurer
    - location: City
      weight: 30
      traits: Brewer, Merchant
- homeland:
  race: human
  table:
    - location: Village
      weight: 30
      traits: Farmer, Rancher
    - location: City
      weight: 70
      traits: Merchant, Politician
";
    }
}

