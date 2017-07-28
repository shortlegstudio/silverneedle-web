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

    public class MagicTests : DomainTestBase<Magic>
    {
        public MagicTests()
        {
            base.InitializeDomain("magic");
        }

        [Fact]
        public void HandOfTheAcolyte()
        {
            var touch = character.Get<HandOfTheAcolyte>();
            Assert.NotNull(touch); 
            Assert.Equal(touch.UsesPerDay, 6);
        }

        [Fact]
        public void DispellingTouch()
        {
            character.SetLevel(8);
            domain.LeveledUp(character.Components);
            var dispel = character.Get<DispellingTouch>();
            Assert.NotNull(dispel);
            Assert.Equal(dispel.UsesPerDay, 1);
            character.SetLevel(16);

            Assert.Equal(dispel.UsesPerDay, 3);
        }
    }
}