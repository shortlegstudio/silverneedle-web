// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;

    public class EnchantingSmileTests
    {
        private CharacterSheet wizard;
        private EnchantingSmile smile;
        public EnchantingSmileTests()
        {
            wizard = CharacterTestTemplates.Wizard();
            smile = new EnchantingSmile();
            wizard.Add(smile);
        }

        [Fact]
        public void ProvidesABonusToBluffChecks()
        {
            Assert.Equal(2, wizard.SkillRanks.GetScore("Bluff"));
            wizard.SetLevel(10);
            Assert.Equal(4, wizard.SkillRanks.GetScore("Bluff"));
        }

        [Fact]
        public void ProvidesABonusToDiplomacyChecks()
        {
            Assert.Equal(2, wizard.SkillRanks.GetScore("Diplomacy"));
            wizard.SetLevel(10);
            Assert.Equal(4, wizard.SkillRanks.GetScore("Diplomacy"));
        }

        [Fact]
        public void ProvidesABonusToIntimidateChecks()
        {
            Assert.Equal(2, wizard.SkillRanks.GetScore("Intimidate"));
            wizard.SetLevel(10);
            Assert.Equal(4, wizard.SkillRanks.GetScore("Intimidate"));
        }
    }

}