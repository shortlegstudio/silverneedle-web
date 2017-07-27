// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CreateMagicItems
{
    using NUnit.Framework;
    using SilverNeedle.Actions.CreateMagicItems;
    using SilverNeedle.Equipment;
    using SilverNeedle.Serialization;
    using SilverNeedle.Spells;

    [TestFixture]
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
            Assert.That(wand.Spell, Is.EqualTo(cure));
            Assert.That(wand.Charges, Is.EqualTo(50));
        }
    }
}