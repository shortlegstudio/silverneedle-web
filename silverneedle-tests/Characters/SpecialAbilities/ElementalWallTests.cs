// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using Xunit;
    using SilverNeedle.Characters.SpecialAbilities;

    public class ElementalWallTests
    {
        [Fact]
        public void RoundsPerDayBasedOnLevel()
        {
            var wizard = CharacterTestTemplates.Wizard();
            var wall = new ElementalWall();
            wizard.Add(wall);
            Assert.Equal(1, wall.RoundsPerDay);
            wizard.SetLevel(10);
            Assert.Equal(10, wall.RoundsPerDay);
            Assert.Equal("Elemental Wall (10 rounds/day)", wall.Name);
        }
    }
}