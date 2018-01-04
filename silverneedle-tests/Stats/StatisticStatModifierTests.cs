// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Stats
{
    using Xunit;
    using SilverNeedle;
    using SilverNeedle.Characters;
    using SilverNeedle.Serialization;

    public class StatisticStatModifierTests
    {
        [Fact]
        public void LoadedFromYamlSpecificationWillProperlyFindStatistic()
        {
            var character = CharacterTestTemplates.AverageBob();
            character.AbilityScores.SetScore(AbilityScoreTypes.Strength, 16);
            var yaml = @"---
name: hit points
modifier: strength-modifier
modifier-type: racial
condition: rain
";
            var modifier = new StatisticStatModifier(yaml.ParseYaml());
            character.Add(modifier);
            Assert.Equal(3, modifier.Modifier);
            Assert.Equal("hit points", modifier.StatisticName);
            Assert.Equal("racial", modifier.ModifierType);
            Assert.Equal("rain", modifier.Condition);
        }

    }
}