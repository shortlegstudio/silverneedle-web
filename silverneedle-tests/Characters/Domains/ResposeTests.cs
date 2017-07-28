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

    public class ReposeTests : DomainTestBase<Repose>
    {
        public ReposeTests()
        {
            base.InitializeDomain("repose");
        }

        [Fact]
        public void GentleRest()
        {
            var touch = character.Get<GentleRest>();
            Assert.NotNull(touch); 
            Assert.Equal(touch.UsesPerDay, 6);
        }

        [Fact]
        public void WardAgainstDeath()
        {
            character.SetLevel(8);
            domain.LeveledUp(character.Components);
            var aura = character.Get<WardAgainstDeath>();
            Assert.NotNull(aura); 
        }
    }
}