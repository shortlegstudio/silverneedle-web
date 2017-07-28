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

    public class LuckTests : DomainTestBase<Luck>
    {
        public LuckTests()
        {
            base.InitializeDomain("luck");
        }

        [Fact]
        public void BitOfLuck()
        {
            var touch = character.Get<BitOfLuck>();
            Assert.NotNull(touch);
            Assert.Equal(touch.UsesPerDay, 6);
        }

        [Fact]
        public void GoodFortune()
        {
            character.SetLevel(6);
            domain.LeveledUp(character.Components);
            var goodFortune = character.Get<GoodFortune>();
            Assert.NotNull(goodFortune);
        }
    }
}