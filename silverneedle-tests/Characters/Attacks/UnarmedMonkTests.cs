// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.Attacks
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Attacks;

    public class UnarmedMonkTests
    {
        [Fact]
        public void CanBeConfiguredForDamage()
        {

            var attack = new UnarmedMonk("1d6");
            Assert.Equal("1d6", attack.Damage.ToString());
        }

        [Fact]
        public void ReturnsAttackInCleanFormat()
        {
            var attack = new UnarmedMonk("1d6");
            Assert.Equal("Unarmed (1d6)", attack.ToString());
        }
    }
}