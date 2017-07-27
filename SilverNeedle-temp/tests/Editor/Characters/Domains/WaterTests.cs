// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.Domains
{
    using System.Linq;
    using NUnit.Framework;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Attacks;
    using SilverNeedle.Characters.Domains;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    [TestFixture]
    public class WaterTests : DomainTestBase<Water>
    {
        [SetUp]
        public void Configure()
        {
            InitializeDomain("water");
        }

        [Fact]
        public void IcicleAttack()
        {
            var acidDart = character.Offense.Attacks().OfType<Icicle>().First();
            Assert.That(acidDart.UsesPerDay, Is.EqualTo(6));
            Assert.That(acidDart.Damage.ToString(), Is.EqualTo("1d6+1"));
            Assert.That(acidDart.ToString(), Is.EqualTo("Icicle 30' (1d6+1 cold)"));
        }

        [Fact]
        public void AddsColdDamageResistance()
        {
            // Level Up Notification
            character.SetLevel(6);
            domain.LeveledUp(character.Components);

            var acid = character.Defense.DamageResistance.First();
            Assert.That(acid.DamageType, Is.EqualTo("cold"));
            Assert.That(acid.Amount, Is.EqualTo(10));

            character.SetLevel(12);
            domain.LeveledUp(character.Components);
            Assert.That(acid.Amount, Is.EqualTo(20));

            //At level 20 remove DR and add immunity
            character.SetLevel(20);
            domain.LeveledUp(character.Components);

            Assert.That(acid.Amount, Is.EqualTo(0));
            var immune = character.Defense.Immunities.First();
            Assert.That(immune.Condition, Is.EqualTo("cold"));

        }


    }
}