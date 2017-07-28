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

    public class DeathTests : DomainTestBase<Death>
    {
        public DeathTests()
        {
            base.InitializeDomain("death");
        }

        [Fact]
        public void BleedingTouch()
        {
            var touch = character.Get<BleedingTouch>();
            Assert.NotNull(touch);
            Assert.Equal(touch.UsesPerDay, 6);
        }

        [Fact]
        public void DeathEmbrace()
        {
            character.SetLevel(8);
            domain.LeveledUp(character.Components);
            var deathEmbrace = character.Get<DeathEmbrace>();
            Assert.NotNull(deathEmbrace);
        }
    }
}