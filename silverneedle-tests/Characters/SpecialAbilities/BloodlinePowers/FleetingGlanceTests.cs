// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities.BloodlinePowers
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities.BloodlinePowers;

    public class FleetingGlanceTests
    {
        [Fact]
        public void RoundsPerDayBasedOnLevel()
        {
            var sorcerer = CharacterTestTemplates.Sorcerer();
            sorcerer.SetLevel(15);
            var glance = new FleetingGlance();
            sorcerer.Add(glance);
            Assert.Equal(15, glance.RoundsPerDay);
        }
    }
}