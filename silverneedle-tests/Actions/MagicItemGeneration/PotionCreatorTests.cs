// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.MagicItemGeneration
{
    using Xunit;
    using SilverNeedle.Actions.MagicItemGeneration;
    using SilverNeedle.Equipment;
    using SilverNeedle.Spells;
    using SilverNeedle.Serialization;

    public class PotionCreatorTests
    {
        [Fact]
        public void SelectAPotionFromAListOfSpells()
        {
            var spellList = new SpellList();
            spellList.Class = "Wizard";
            spellList.Add(1, "Cure Light Wounds");
            var cureSpell = new Spell("Cure Light Wounds", "healing");
            var spellLists = EntityGateway<SpellList>.LoadWithSingleItem(spellList);
            var spells = EntityGateway<Spell>.LoadWithSingleItem(cureSpell);

            var potionCreator = new PotionCreator(spellLists, spells);
            var potion = potionCreator.Process();

            Assert.Equal(cureSpell, potion.Spell);
        }
    }
}