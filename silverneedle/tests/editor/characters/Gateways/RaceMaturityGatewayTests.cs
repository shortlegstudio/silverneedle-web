// //-----------------------------------------------------------------------
// // <copyright file="RaceMaturityYamlGatewayTests.cs" company="Short Leg Studio, LLC">
// //     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------
using NUnit.Framework;
using SilverNeedle.Characters;
using SilverNeedle;
using System.IO;
using System.Linq;
using SilverNeedle.Dice;
using SilverNeedle.Yaml;

namespace Characters {

    [TestFixture]
    public class RaceMaturityGatewayTests {
        Maturity human;
        Maturity dwarf;
        RaceMaturityGateway gateway;

        [SetUp]
        public void SetUp() {
            gateway = new RaceMaturityGateway (MaturityDataFile.ParseYaml());
            human = gateway.All().First(x => x.Name == "human");
            dwarf = gateway.All().First(x => x.Name == "dwarf");
        }

        [Test]
        public void CanRetreivewMaturityByRaceClass()
        {
            var race = new Race();
            race.Name = "Human";
            Assert.AreEqual(human, gateway.Get(race));
        }

        [Test]
        public void MaturitiesHaveDifferentAttributesForDifferentProfessions()
        {
            Assert.AreEqual(15, human.Adulthood);
            Assert.IsInstanceOf(typeof(Cup), human.Young);
            Assert.AreEqual(40, dwarf.Adulthood);
        }


        private const string MaturityDataFile = @"--- 
- maturity:
    race: human
    adulthood: 15
    young: 1d4
    trained: 1d6
    studied: 2d6
- maturity:
    race: dwarf
    adulthood: 40
    young: 3d6
    trained: 5d6
    studied: 7d6
...";
    }
}