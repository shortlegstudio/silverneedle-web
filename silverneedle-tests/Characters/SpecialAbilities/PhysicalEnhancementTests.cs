// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;

    public class PhysicalEnhancementTests
    {
        [Fact]
        public void ChooseAnAbilityAndApplyTheModifierToIt()
        {
            var wizard = CharacterTestTemplates.Wizard();
            var enhance = new PhysicalEnhancement();
            wizard.Add(enhance);

            //Modifiers have to equal one with the bonus
            Assert.Equal
            (
                31,
                wizard.AbilityScores.GetScore(AbilityScoreTypes.Strength) +
                wizard.AbilityScores.GetScore(AbilityScoreTypes.Dexterity) + 
                wizard.AbilityScores.GetScore(AbilityScoreTypes.Constitution)
            );

            wizard.SetLevel(15);

            Assert.Equal
            (
                34,
                wizard.AbilityScores.GetScore(AbilityScoreTypes.Strength) +
                wizard.AbilityScores.GetScore(AbilityScoreTypes.Dexterity) + 
                wizard.AbilityScores.GetScore(AbilityScoreTypes.Constitution)
            );
        }
    }
}