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

    public class GloryTests : DomainTestBase<Glory>
    {
        public GloryTests()
        {
            base.InitializeDomain("glory");
        }

        [Fact]
        public void TouchOfGlory()
        {
            var touch = character.Get<TouchOfGlory>();
            Assert.NotNull(touch); 
            Assert.Equal(touch.UsesPerDay, 6);
        }

        [Fact]
        public void DivinePresence()
        {
            character.SetLevel(8);
            domain.LeveledUp(character.Components);
            var divinePres = character.Get<DivinePresence>();
            Assert.NotNull(divinePres); 
        }
    }
}