// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Core
{
    using SilverNeedle;
    using SilverNeedle.Characters;
    using SilverNeedle.Serialization;
    using Xunit;

    public class StatisticDifferenceModifierTests
    {
        [Fact]
        public void AppliesTheDifferenceBetweenTwoStats()
        {
            var yaml = @"
name: Some Stat
modifier-type: base-value
base-statistic: strength-modifier
difference-statistic: dexterity-modifier";
            var mod = new StatisticDifferenceModifier(yaml.ParseYaml());
            var bob = CharacterTestTemplates.AverageBob();
            bob.AbilityScores.SetScore(AbilityScoreTypes.Strength, 12);
            bob.AbilityScores.SetScore(AbilityScoreTypes.Dexterity, 14);
            bob.Add(mod);
            Assert.Equal(1, mod.Modifier);

        }
    }
}