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

    public class CharmTests : DomainTestBase<Charm>
    {
        public CharmTests()
        {
            base.InitializeDomain("charm");
        }

        [Fact]
        public void CanDazeCreations()
        {
            var touch = character.Get<DazingTouch>();
            Assert.NotNull(touch);
            Assert.Equal(touch.UsesPerDay, 6);
        }

        [Fact]
        public void CharmingSmile()
        {
            character.SetLevel(8);
            domain.LeveledUp(character.Components);
            var charmingSmile = character.Get<CharmingSmile>();
            Assert.NotNull(charmingSmile);
        }
    }
}