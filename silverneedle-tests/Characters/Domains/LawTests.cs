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

    public class LawTests : DomainTestBase<Law>
    {
        public LawTests()
        {
            base.InitializeDomain("law");
        }

        [Fact]
        public void CanTouchOfLaw()
        {
            var touch = character.Get<TouchOfLaw>();
            Assert.NotNull(touch);
            Assert.Equal(touch.UsesPerDay, 6);
        }

        [Fact]
        public void CanMakeWeaponsLawful()
        {
            character.SetLevel(8);
            domain.LeveledUp(character.Components);
            var staffOrder = character.Get<StaffOfOrder>();
            Assert.NotNull(staffOrder);
            Assert.Equal(staffOrder.UsesPerDay, 1);
            character.SetLevel(16);

            Assert.Equal(staffOrder.UsesPerDay, 3);
        }
    }
}