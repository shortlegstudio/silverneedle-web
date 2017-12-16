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
        [Theory]
        [InlineData(1, 5000)]
        [InlineData(2, 30000)]
        [InlineData(3, 75000)]
        public void SelectAPotionFromAListOfSpellsAndChargesProperly(int level, int value)
        {
            var spellList = SpellList.CreateForTesting("cleric");
            spellList.Add(level, "Cure Light Wounds");
            var cureSpell = new Spell("Cure Light Wounds", "healing");
            var spellLists = EntityGateway<SpellList>.LoadWithSingleItem(spellList);
            var spells = EntityGateway<Spell>.LoadWithSingleItem(cureSpell);

            var potionCreator = new PotionCreator(spellLists, spells);
            var potion = potionCreator.Process();

            Assert.Equal(cureSpell, potion.Spell);
            Assert.Equal(value, potion.Value);
        }

        [Theory]
        [Repeat(100)]
        public void MaxSpellLevelIsLevel3()
        {
            var spellList = SpellList.CreateForTesting("cleric");
            spellList.Add(1, "Cure Light Wounds");
            spellList.Add(4, "Cure Serious Wounds");
            var cureSpell = new Spell("Cure Light Wounds", "healing");
            var cureSeriousSpell = new Spell("Cure Serious Wounds", "healing");
            var spellLists = EntityGateway<SpellList>.LoadWithSingleItem(spellList);
            var spells = EntityGateway<Spell>.LoadFromList(new Spell[] { cureSpell, cureSeriousSpell });

            var potionCreator = new PotionCreator(spellLists, spells);
            var potion = potionCreator.Process();

            //Always equals cureSpell
            Assert.Equal(cureSpell, potion.Spell);
        }


    }
}