// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using Xunit;
    using SilverNeedle.Characters.SpecialAbilities;

    public class InvisibilityFieldTests
    {
        [Fact]
        public void RoundsPerDayBasedOnLevel()
        {
            var wizard = CharacterTestTemplates.Wizard();
            var field = new InvisibilityField();
            wizard.Add(field);
            Assert.Equal(1, field.RoundsPerDay);
            wizard.SetLevel(10);
            Assert.Equal(10, field.RoundsPerDay);
            Assert.Equal("Invisibility Field (10 rounds/day)", field.Name);

        }
    }
}