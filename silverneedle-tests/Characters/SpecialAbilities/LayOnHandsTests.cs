// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;

    
    public class LayOnHandsTests 
    {
        private CharacterSheet level4Paladin;
        public LayOnHandsTests()
        {
            level4Paladin = new CharacterSheet(CharacterStrategy.Default());
            level4Paladin.AbilityScores.SetScore(AbilityScoreTypes.Charisma, 14); //+2 Modifier
            var paladin = new Class();
            paladin.Name = "Paladin";
            level4Paladin.SetClass(paladin);
            level4Paladin.SetLevel(4);
            var layOnHands = new LayOnHands();
            level4Paladin.Add(layOnHands);
        }

        [Fact]
        public void LayOnHandsCanBeUsedANumberOfTimesBasedOnCharismaAndPaladinLevels()
        {
            // Charisma + 1/2 Paladin Level 
            Assert.Equal(level4Paladin.Get<LayOnHands>().UsesPerDay, 4);
        }

        [Fact]
        public void LayOnHandsHealingIsBasedOnLevel()
        {
            Assert.Equal(level4Paladin.Get<LayOnHands>().HealingDice.ToString(), "2d6");
        }

        [Fact]
        public void NameReflectsUsesAndHealing()
        {
            Assert.Equal(level4Paladin.Get<LayOnHands>().DisplayString(), "Lay on Hands (2d6, 4/day)");
        }

        [Fact]
        public void CanBeSetToAlwaysReturnTheMaximumValue()
        {
            var layOn = level4Paladin.Get<LayOnHands>();
            layOn.MaximizeAmount = true;
            Assert.Equal(layOn.HealingDice.Roll(), 12);
            Assert.Equal(layOn.DisplayString(), "Lay on Hands (12 points, 4/day)");
        }
    }
}