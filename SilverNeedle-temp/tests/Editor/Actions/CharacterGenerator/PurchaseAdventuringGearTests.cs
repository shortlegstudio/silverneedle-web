// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGenerator
{
    using System.Collections.Generic;
    using System.Linq;
    using NUnit.Framework;
    using SilverNeedle.Actions.CharacterGenerator;
    using SilverNeedle.Characters;
    using SilverNeedle.Equipment;
    using SilverNeedle.Serialization;

    [TestFixture]
    public class PurchasingAdventuringGearTests
    {
        EntityGateway<Gear> gearGateway;
        PurchaseAdventuringGear subject;

        [SetUp]
        public void SetUpTests()
        {
            var gears = new List<Gear>();
            gears.Add(new Gear("tool", 20, 1));
            gears.Add(new Gear("sack", 20, 1));
            gears.Add(new Gear("backpack", 20, 1));
            gears.Add(new Gear("Flint and steel", 20, 1));
            gears.Add(new Gear("chalk", 20, 1));
            gears.Add(new Gear("other thing", 20, 1));
            gears.Add(new Gear("some stuff", 20, 1));
            gears.Add(new Gear("doodad", 20, 1));
            gearGateway = new EntityGateway<Gear>(gears);
            
            subject = new PurchaseAdventuringGear(gearGateway);
        }

        [Test]
        public void PicksUpSomeRandomGearWorthLessThan20000()
        {
            var character = new CharacterSheet();
            character.Inventory.CoinPurse.SetValue(50000);

            subject.Process(character, new CharacterBuildStrategy());
            Assert.That(character.Inventory.CoinPurse.Value, Is.AtLeast(30000));
            Assert.That(character.Inventory.All.Count, Is.AtLeast(1));
        }

        [Test]
        public void DoNotSpendMoneyYouDoNotHave()
        {
            var character = new CharacterSheet();
            character.Inventory.CoinPurse.SetValue(200);
            subject.Process(character, new CharacterBuildStrategy());
            Assert.That(character.Inventory.All.Count(), Is.AtLeast(1));
        }

        [Test]
        public void IfNothingIsAffordableStop()
        {
            var character = new CharacterSheet();
            character.Inventory.CoinPurse.SetValue(1);
            subject.Process(character, new CharacterBuildStrategy());
            Assert.That(character.Inventory.All, Is.Empty);
        }
    }
}