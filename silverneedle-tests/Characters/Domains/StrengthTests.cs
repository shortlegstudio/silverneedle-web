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

    public class StrengthTests : DomainTestBase<Strength>
    {
        public StrengthTests()
        {
            base.InitializeDomain("strength");
        }

        [Fact]
        public void StrengthSurge()
        {
            var touch = character.Get<StrengthSurge>();
            Assert.NotNull(touch); 
            Assert.Equal(touch.UsesPerDay, 6);
        }

        [Fact]
        public void MightOfGods()
        {
            character.SetLevel(8);
            domain.LeveledUp(character.Components);
            var aura = character.Get<MightOfTheGods>();
            Assert.NotNull(aura); 
        }
    }
}