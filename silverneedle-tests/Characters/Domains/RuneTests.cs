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

    public class RuneTests : DomainTestBase<Rune>
    {
        public RuneTests()
        {
            base.InitializeDomain("rune");
        }

        [Fact]
        public void BlastRune()
        {
            var touch = character.Get<BlastRune>();
            Assert.NotNull(touch); 
            Assert.Equal(touch.UsesPerDay, 6);
        }

        [Fact]
        public void SpellRune()
        {
            character.SetLevel(8);
            domain.LeveledUp(character.Components);
            var aura = character.Get<SpellRune>();
            Assert.NotNull(aura); 
        }
    }
}