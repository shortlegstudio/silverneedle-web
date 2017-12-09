// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using Xunit;
    using SilverNeedle.Characters.SpecialAbilities;

    public class DimensionalStepsTests
    {
        [Fact]
        public void GetThirtyFeetTeleportPerLevel()
        {
            var wizard = CharacterTestTemplates.Wizard();
            var dimSteps = new DimensionalSteps();
            wizard.Add(dimSteps);
            Assert.Equal(30, dimSteps.Distance);
            wizard.SetLevel(10);
            Assert.Equal(300, dimSteps.Distance);
            Assert.Equal("Dimensional Steps (300 ft/day)", dimSteps.Name);

        }
    }
}