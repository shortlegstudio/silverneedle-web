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

    public class LiberationTests : DomainTestBase<Liberation>
    {
        public LiberationTests()
        {
            base.InitializeDomain("liberation");
        }

        [Fact]
        public void Liberation()
        {
            var touch = character.Get<LiberationMobility>();
            Assert.NotNull(touch);
        }

        [Fact]
        public void FreedomCall()
        {
            character.SetLevel(8);
            domain.LeveledUp(character.Components);
            var free = character.Get<FreedomCall>();
            Assert.NotNull(free);
        }
    }
}