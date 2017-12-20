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

    
    public class HolyChampionTests
    {
        public CharacterSheet paladin;
        public HolyChampionTests()
        {
            this.paladin = CharacterTestTemplates.AverageBob();
            var cls = new Class();
            cls.Name = "Paladin";
            paladin.SetClass(cls);
            paladin.SetLevel(20);
            paladin.Add(new LayOnHands());
            paladin.Add(new ChannelEnergy());
            paladin.Add(new HolyChampion());
        }
        [Fact]
        public void AddsDamageResistance()
        {
            var def = paladin.Get<DefenseStats>();
            var dr = def.DamageResistance.First();
            Assert.Equal(dr.Amount, 5);
            Assert.Equal(dr.DamageType, "evil");
        }

        [Fact]
        public void SetsLayOnHandsAndChannelToMaximumPower()
        {
            var lay = paladin.Get<LayOnHands>();
            Assert.Equal(lay.HealingDice.ToString(), "60 points");
            
            var channel = paladin.Get<ChannelEnergy>();
            Assert.Equal(channel.ChannelAttack.Damage.ToString(), "60 points");
        }
    }
}