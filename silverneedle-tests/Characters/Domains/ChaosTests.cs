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

    public class ChaosTests : DomainTestBase<Chaos>
    {
        public ChaosTests()
        {
            base.InitializeDomain("chaos");
        }

        [Fact]
        public void CanTouchOfChaos()
        {
            var touch = character.Get<TouchOfChaos>();
            Assert.NotNull(touch);
            Assert.Equal(touch.UsesPerDay, 6);
        }

        [Fact]
        public void CanMakeWeaponsChaotic()
        {
            character.SetLevel(8);
            domain.LeveledUp(character.Components);
            var chaosBlade = character.Get<ChaosBlade>();
            Assert.NotNull(chaosBlade);
            Assert.Equal(chaosBlade.UsesPerDay, 1);
            character.SetLevel(16);

            Assert.Equal(chaosBlade.UsesPerDay, 3);
        }
    }
}