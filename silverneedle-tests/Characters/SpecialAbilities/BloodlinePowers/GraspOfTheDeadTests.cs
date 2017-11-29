// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities.BloodlinePowers
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities.BloodlinePowers;

    public class GraspOfTheDeadTests
    {
        private CharacterSheet sorcerer;
        private GraspOfTheDead grasp;

        public GraspOfTheDeadTests()
        {
            sorcerer = CharacterTestTemplates.Sorcerer();
            grasp = new GraspOfTheDead();
            sorcerer.Add(grasp);
        }

        [Fact]
        public void DamageIsDependentOnLevel()
        {
            sorcerer.SetLevel(10);
            Assert.Equal("10d6", grasp.Damage.ToString());
            Assert.Equal("slashing", grasp.DamageType);
        }

        [Fact]
        public void SaveDCIsBasedOnLevelAndCharisma()
        {
            sorcerer.SetLevel(10);
            sorcerer.AbilityScores.SetScore(AbilityScoreTypes.Charisma, 16);
            Assert.Equal(18, grasp.SaveDC);

        }

        [Fact]
        public void DisplayStringContainsRelevantInformation()
        {
            Assert.Equal("1/day Grasp of the Dead 20' radius (1d6 slashing, DC 10)", grasp.DisplayString());
        }
    }
}