// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.Domains
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Domains;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Serialization;

    public class HealingTests : DomainTestBase<Healing>
    {
        public HealingTests()
        {
            base.InitializeDomain("healing");
        }

        [Fact]
        public void RebukeDeath()
        {
            var touch = character.Get<RebukeDeath>();
            Assert.NotNull(touch);
            Assert.Equal(touch.UsesPerDay, 6);
        }

        [Fact]
        public void HealerBlessing()
        {
            character.SetLevel(6);
            domain.LeveledUp(character.Components);
            var healBless = character.Get<HealerBlessing>();
            Assert.NotNull(healBless); 
        }
    }
}