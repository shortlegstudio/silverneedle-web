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

    public class GoodTests : DomainTestBase<Good>
    {
        public GoodTests()
        {
            base.InitializeDomain("good");
        }

        [Fact]
        public void TouchOfGood()
        {
            var touch = character.Get<TouchOfGood>();
            Assert.NotNull(touch); 
            Assert.Equal(touch.UsesPerDay, 6);
        }

        [Fact]
        public void HolyLance()
        {
            character.SetLevel(8);
            domain.LeveledUp(character.Components);
            var evilScythe = character.Get<HolyLance>();
            Assert.NotNull(evilScythe); 
            Assert.Equal(evilScythe.UsesPerDay, 1);
            character.SetLevel(16);

            Assert.Equal(evilScythe.UsesPerDay, 3);
        }
    }
}