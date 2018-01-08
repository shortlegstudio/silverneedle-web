// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using System.Linq;
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    
    
    public class ChannelEnergyTests
    {
        [Fact]
        public void ChannelEnergyAddsASpecialAttack()
        {
            var channel = new ChannelEnergy();
            var character = new CharacterSheet(CharacterStrategy.Default());
            character.InitializeComponents();
            character.AbilityScores.SetScore(AbilityScoreTypes.Charisma, 12);
            var cls = new Class();
            cls.Name = "Cleric";
            character.SetClass(cls);
            character.SetLevel(4);
            character.Add(channel);
            var channelAttack = character.Offense.Attacks().First(x => x.Name.Contains("Channel"));
            Assert.NotNull(channelAttack);
            Assert.Equal(channelAttack.Damage.ToString(), "2d6");
            Assert.Equal(channelAttack.SaveDC, 13);
        }

    }
}