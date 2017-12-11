// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using Xunit;
    using SilverNeedle.Characters.SpecialAbilities;

    public class ChangeShapeTests
    {
        [Fact]
        public void RoundsPerDayBasedOnLevel()
        {
            var wizard = CharacterTestTemplates.Wizard();
            var change = new ChangeShape();
            wizard.Add(change);
            Assert.Equal(1, change.RoundsPerDay);
            wizard.SetLevel(15);
            Assert.Equal(15, change.RoundsPerDay);
        }
    }
}