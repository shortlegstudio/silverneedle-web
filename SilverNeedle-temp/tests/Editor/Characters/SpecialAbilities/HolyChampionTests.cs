// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using System.Linq;
    using NUnit.Framework;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;

    [TestFixture]
    public class HolyChampionTests
    {
        public CharacterSheet paladin;
        [SetUp]
        public void Configure()
        {
            this.paladin = new CharacterSheet();
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
            Assert.That(dr.Amount, Is.EqualTo(5));
            Assert.That(dr.DamageType, Is.EqualTo("evil"));
        }

        [Fact]
        public void SetsLayOnHandsAndChannelToMaximumPower()
        {
            var lay = paladin.Get<LayOnHands>();
            Assert.That(lay.HealingDice.ToString(), Is.EqualTo("60 points"));
            
            var channel = paladin.Get<ChannelEnergy>();
            Assert.That(channel.ChannelAttack.Damage.ToString(), Is.EqualTo("60 points"));
        }
    }
}