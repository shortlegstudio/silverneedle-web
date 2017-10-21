// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGeneration
{
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;
    using SilverNeedle.Actions.CharacterGeneration;
    using SilverNeedle.Characters;
    using SilverNeedle.Equipment;
    using SilverNeedle.Serialization;

    
    public class PurchasingAdventuringGearTests
    {
        EntityGateway<Gear> gearGateway;
        PurchaseAdventuringGear subject;

        public PurchasingAdventuringGearTests()
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
            gearGateway = EntityGateway<Gear>.LoadFromList(gears);
            
            subject = new PurchaseAdventuringGear(gearGateway);
        }

        [Fact]
        public void PicksUpSomeRandomGearWorthLessThan20000()
        {
            var character = new CharacterSheet(CharacterStrategy.Default());
            character.Inventory.CoinPurse.SetValue(50000);
            var strategy = new CharacterStrategy();

            subject.ExecuteStep(character, strategy);
            Assert.True(character.Inventory.CoinPurse.Value >= 30000);
            Assert.True(character.Inventory.All.Count() >= 1);
        }

        [Fact]
        public void DoNotSpendMoneyYouDoNotHave()
        {
            var character = new CharacterSheet(CharacterStrategy.Default());
            character.Inventory.CoinPurse.SetValue(200);
            subject.ExecuteStep(character, new CharacterStrategy());
            Assert.True(character.Inventory.All.Count() >= 1);
        }

        [Fact]
        public void IfNothingIsAffordableStop()
        {
            var character = new CharacterSheet(CharacterStrategy.Default());
            character.Inventory.CoinPurse.SetValue(1);
            subject.ExecuteStep(character, new CharacterStrategy());
            Assert.Empty(character.Inventory.All);
        }
    }
}