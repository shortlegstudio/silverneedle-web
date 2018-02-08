// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using System.Linq;
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Attacks;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Serialization;
    
    public class ChannelEnergyTests
    {
        ChannelEnergy channel;
        public ChannelEnergyTests()
        {
            var yaml = @"---
save-dc-stat:
  name: Channel Energy Save DC
  base-value: 10
dice-stat:
  name: Channel Energy Dice
  dice: 1d6
uses-per-day-stat:
  name: Channel Energy Uses Per Day
  base-value: 3";
            channel = new ChannelEnergy(yaml.ParseYaml());
        }
        [Fact]
        public void ChannelEnergyAddsASpecialAttack()
        {
            var character = new CharacterSheet(CharacterStrategy.Default());
            character.InitializeComponents();
            character.AbilityScores.SetScore(AbilityScoreTypes.Charisma, 12);
            var cls = new Class();
            cls.Name = "Cleric";
            character.SetClass(cls);
            character.SetLevel(4);
            character.Add(channel);
            Assert.Equal(channel.Damage.ToString(), "1d6");
            var channelAttack = character.Offense.Attacks().First(x => x.Name.Contains("Channel"));
            Assert.NotNull(channelAttack);
        }

        [Fact]
        public void SaveDCandDamageAreAddedToTheLargerComponentPool()
        {
            var character = CharacterTestTemplates.Cleric();
            character.Add(channel);
            Assert.NotNull(character.FindStat("Channel Energy Save DC"));
            Assert.NotNull(character.FindStat("Channel Energy Dice"));
            Assert.NotNull(character.FindStat("Channel Energy Uses Per Day"));
        }

        [Fact]
        public void PicksPositiveEnergyIfAlignmentIsGood()
        {
            var c = CharacterTestTemplates.Cleric();
            c.Alignment = CharacterAlignment.ChaoticGood;
            c.Add(channel);
            Assert.Equal(ChannelEnergy.POSITIVE_ENERGY, channel.EnergyType);
        }

        [Fact]
        public void PicksNegativeEnergyIfAlignmentIsEvil()
        {
            var c = CharacterTestTemplates.Cleric();
            c.Alignment = CharacterAlignment.LawfulEvil;
            c.Add(channel);
            Assert.Equal(ChannelEnergy.NEGATIVE_ENERGY, channel.EnergyType);
        }


        [Fact]
        public void NeutralCharactersPickSomething()
        {
            var c = CharacterTestTemplates.Cleric();
            c.Alignment = CharacterAlignment.Neutral;
            c.Add(channel);
            Assert.False(string.IsNullOrEmpty(channel.EnergyType));
        }

    }
}