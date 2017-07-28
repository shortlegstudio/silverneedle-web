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

    public class CommunityTests : DomainTestBase<Community>
    {
        public CommunityTests()
        {
            base.InitializeDomain("community");
        }

        [Fact]
        public void CanCalmingTouch()
        {
            var touch = character.Get<CalmingTouch>();
            Assert.NotNull(touch);
            Assert.Equal(touch.UsesPerDay, 6);
        }

        [Fact]
        public void CanGiveUnity()
        {
            character.SetLevel(8);
            domain.LeveledUp(character.Components);
            var unity = character.Get<Unity>();
            Assert.NotNull(unity);
            
            Assert.Equal(unity.UsesPerDay, 1);
            character.SetLevel(16);

            Assert.Equal(unity.UsesPerDay, 3);
        }
    }
}