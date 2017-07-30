// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CreateMagicItems
{
    using Xunit;
    using SilverNeedle.Actions.CreateMagicItems;
    using SilverNeedle.Equipment;
    using SilverNeedle.Serialization;
    using SilverNeedle.Spells;

    
    public class CreateWandTests
    {
        [Fact]
        public void WandsUtilizeAvailableListsToMakeWandsThatMakeSense()
        {
            var spellList = new SpellList();
            spellList.Class = "Cleric";
            spellList.Levels.Add(1, new string[] { "Cure Light Wounds" });
            var cure = new Spell("Cure Light Wounds", "healing");
            var spellGateway = new EntityGateway<Spell>(new Spell[] { cure });
            var spellListGateway = new EntityGateway<SpellList>(new SpellList[] { spellList });

            var createwands = new CreateWands(spellGateway, spellListGateway);

            var wand = createwands.Process();
            Assert.Equal(wand.Spell, cure);
            Assert.Equal(wand.Charges, 50);
        }
    }
}