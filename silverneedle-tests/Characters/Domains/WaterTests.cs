// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.Domains
{
    using System.Linq;
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Attacks;
    using SilverNeedle.Characters.Domains;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    
    public class WaterTests : DomainTestBase<Water>
    {
        public WaterTests()
        {
            InitializeDomain("water");
        }

        [Fact]
        public void IcicleAttack()
        {
            var icicle = character.Offense.Attacks().OfType<Icicle>().First();
            Assert.Equal(icicle.UsesPerDay, 6);
            Assert.Equal(icicle.Damage.ToString(), "1d6+1");
            Assert.Equal(icicle.DisplayString(), "Icicle 30' (1d6+1 cold)");
        }

        [Fact]
        public void AddsColdDamageResistance()
        {
            // Level Up Notification
            character.SetLevel(6);
            domain.LeveledUp(character.Components);

            var acid = character.Defense.DamageResistance.First();
            Assert.Equal(acid.DamageType, "cold");
            Assert.Equal(acid.Amount, 10);

            character.SetLevel(12);
            domain.LeveledUp(character.Components);
            Assert.Equal(acid.Amount, 20);

            //At level 20 remove DR and add immunity
            character.SetLevel(20);
            domain.LeveledUp(character.Components);

            Assert.Equal(acid.Amount, 0);
            AssertCharacter.IsImmuneTo("cold", character);
        }
    }
}