// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.MagicItemGeneration
{
    using Xunit;
    using SilverNeedle.Actions.MagicItemGeneration;
    using SilverNeedle.Equipment;
    using SilverNeedle.Serialization;
    using SilverNeedle.Spells;
    using SilverNeedle.Treasure;

    
    public class WandCreatorTests
    {
        [Fact]
        public void WandsUtilizeAvailableListsToMakeWandsThatMakeSense()
        {
            var spellList = new SpellList();
            spellList.Class = "Cleric";
            spellList.Levels.Add(1, new string[] { "Cure Light Wounds" });
            var cure = new Spell("Cure Light Wounds", "healing");
            var spellGateway = EntityGateway<Spell>.LoadWithSingleItem(cure);
            var spellListGateway = EntityGateway<SpellList>.LoadWithSingleItem(spellList); 

            var createwands = new WandCreator(spellGateway, spellListGateway);

            var wand = createwands.Process();
            Assert.Equal(cure, wand.Spell);
            Assert.Equal(50, wand.Charges);
            Assert.Equal(75000, wand.Value);
        }

        [Theory]
        [InlineData(1, 75000)]
        [InlineData(2, 450000)]
        [InlineData(3, 1125000)]
        [InlineData(4, 2100000)]
        public void WandsGetMoreExpensiveWithLevels(int level, int expectedValue)
        {
            var spellList = new SpellList();
            spellList.Class = "Cleric";
            spellList.Levels.Add(level, new string[] { "Cure Light Wounds" });
            var cure = new Spell("Cure Light Wounds", "healing");
            var spellGateway = EntityGateway<Spell>.LoadWithSingleItem(cure);
            var spellListGateway = EntityGateway<SpellList>.LoadWithSingleItem(spellList); 

            var createwands = new WandCreator(spellGateway, spellListGateway);

            var wand = createwands.Process();
            Assert.Equal(expectedValue, wand.Value);
        }

    }
}