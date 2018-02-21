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
            Assert.Equal("hit points", modifier.StatisticName);
            Assert.Equal("racial", modifier.ModifierType);
            Assert.Equal("rain", modifier.Condition);
            Assert.Equal(1, modifier.Multiplier);
            Assert.Equal(3, modifier.Modifier);
        }

        [Fact]
        public void CanHaveAMultiplierToAdjustValues()
        {
            var character = CharacterTestTemplates.AverageBob();
            character.AbilityScores.SetScore(AbilityScoreTypes.Strength, 16);
            var yaml = @"---
name: hit points
modifier: strength-modifier
modifier-type: racial
condition: rain
multiplier: 1.5
";
            var modifier = new StatisticStatModifier(yaml.ParseYaml());
            character.Add(modifier);
            Assert.Equal("hit points", modifier.StatisticName);
            Assert.Equal("racial", modifier.ModifierType);
            Assert.Equal("rain", modifier.Condition);
            Assert.Equal(1.5f, modifier.Multiplier);
            Assert.Equal(4.5f, modifier.Modifier);
        }

        [Fact]
        public void IfStatisticToTrackCannotBeFoundThrowException()
        {
            var character = CharacterTestTemplates.AverageBob();
            var yaml = @"---
name: hit points
modifier: some-missing-modifier
modifier-type: racial
condition: rain
multiplier: 1.5
";
            var mod = new StatisticStatModifier(yaml.ParseYaml());
            Assert.Throws(typeof(StatisticNotFoundException), () => { character.Add(mod); });

        }

        [Fact]
        public void SometimesWantToDoForEveryFourInStatAddTwoSomewhereElse()
        {
            var character = CharacterTestTemplates.AverageBob();
            var yaml = @"---
name: power attack
modifier: base attack bonus
modifier-type: circumstance
every: 4
add: 2";
            var statMod = new StatisticStatModifier(yaml.ParseYaml());
            character.Add(statMod);
            Assert.Equal(0, statMod.Modifier);
            character.Offense.BaseAttackBonus.SetValue(3);
            Assert.Equal(0, statMod.Modifier);
            character.Offense.BaseAttackBonus.SetValue(4);
            Assert.Equal(2, statMod.Modifier);
            character.Offense.BaseAttackBonus.SetValue(7);
            Assert.Equal(2, statMod.Modifier);
            character.Offense.BaseAttackBonus.SetValue(8);
            Assert.Equal(4, statMod.Modifier);
            character.Offense.BaseAttackBonus.SetValue(11);
            Assert.Equal(4, statMod.Modifier);
            character.Offense.BaseAttackBonus.SetValue(12);
            Assert.Equal(6, statMod.Modifier);
        }


    }
}