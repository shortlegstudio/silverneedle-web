// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters
{
    using SilverNeedle;
    using SilverNeedle.Characters;
    using SilverNeedle.Serialization;
    using Xunit;

    public class DiceClassLevelModifierTests
    {
        [Fact]
        public void AddsDiceToTheStatisticBasedOnLevelOfCharacter()
        {
            var character = CharacterTestTemplates.Cleric();
            var diceStat = new DiceStatistic("stat-name", "1d6");
            character.Add(diceStat);
            //Example based on channel energy
            var yaml = @"---
name: stat-name
class: cleric
dice: 1d6
rate: 2
start-level: 1";
            var mod = new DiceClassLevelModifier(yaml.ParseYaml());
            character.Add(mod);

            Assert.Equal("1d6", diceStat.DisplayString());
            character.SetLevel(2);
            Assert.Equal("1d6", diceStat.DisplayString());
            character.SetLevel(3);
            Assert.Equal("2d6", diceStat.DisplayString());
            character.SetLevel(4);
            Assert.Equal("2d6", diceStat.DisplayString());
            character.SetLevel(12);
            Assert.Equal("6d6", diceStat.DisplayString());
            character.SetLevel(19);
            Assert.Equal("10d6", diceStat.DisplayString());


        }
    }
}