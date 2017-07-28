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

    public class PlantTests : DomainTestBase<Plant>
    {
        public PlantTests()
        {
            base.InitializeDomain("plant");
        }

        [Fact]
        public void WoodenFist()
        {
            var touch = character.Get<WoodenFist>();
            Assert.NotNull(touch);
            Assert.Equal(touch.UsesPerDay, 6);
        }

        [Fact]
        public void BrambleArmor()
        {
            character.SetLevel(6);
            domain.LeveledUp(character.Components);
            var armor = character.Get<BrambleArmor>();
            Assert.NotNull(armor);
        }
    }
}