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

    public class TravelTests : DomainTestBase<Travel>
    {
        public TravelTests()
        {
            base.InitializeDomain("travel");
        }

        [Fact]
        public void AgileFeet()
        {
            var touch = character.Get<AgileFeet>();
            Assert.NotNull(touch); 
            Assert.Equal(touch.UsesPerDay, 6);
        }

        [Fact]
        public void DimensionalHop()
        {
            character.SetLevel(8);
            domain.LeveledUp(character.Components);
            var aura = character.Get<DimensionalHop>();
            Assert.NotNull(aura);
        }
    }
}