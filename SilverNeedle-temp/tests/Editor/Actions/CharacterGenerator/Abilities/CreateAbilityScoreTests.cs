// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGenerator.Abilities
{
    using NUnit.Framework;
    using SilverNeedle.Actions.CharacterGenerator.Abilities;
    using SilverNeedle.Characters;

    [TestFixture]
    public class CreateAbilityScoreTests
    {
        [Fact]
        public void UsesAbilityScoreGeneratorSpecifiedByStrategy()
        {
            var strategy = new CharacterBuildStrategy();
            strategy.AbilityScoreRoller = "SilverNeedle.Actions.CharacterGenerator.Abilities.AverageAbilityScoreGenerator";
            var character = new CharacterSheet();
            var step = new CreateAbilityScores();
            step.Process(character, strategy);
            Assert.That(character.AbilityScores.GetScore(AbilityScoreTypes.Wisdom), Is.EqualTo(10));
            Assert.That(character.AbilityScores.GetScore(AbilityScoreTypes.Charisma), Is.EqualTo(10));
            Assert.That(character.AbilityScores.GetScore(AbilityScoreTypes.Strength), Is.EqualTo(10));
        }
    }
}