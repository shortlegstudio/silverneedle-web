// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities.BloodlinePowers
{
    using Xunit;
    using SilverNeedle.Characters.SpecialAbilities.BloodlinePowers;

    public class WingsOfHeavenTests
    {
        [Fact]
        public void WingsOfHeavenLastForLevelNumberOfMinutesPerDay()
        {
            var sorcerer = CharacterTestTemplates.Sorcerer();
            var wings = new WingsOfHeaven();
            sorcerer.Add(wings);
            sorcerer.SetLevel(10);
            Assert.Equal(10, wings.MinutesPerDay);
            Assert.Equal("Wings Of Heaven (10 minutes/day)", wings.Name);
        }

        [Fact]
        public void UnlimitedMinutesIfAscended()
        {
            var sorcerer = CharacterTestTemplates.Sorcerer();
            var wings = new WingsOfHeaven();
            sorcerer.Add(wings);
            sorcerer.Add(new Ascension());
            sorcerer.SetLevel(10);
            Assert.Equal(int.MaxValue, wings.MinutesPerDay);
            Assert.Equal("Wings Of Heaven (unlimited minutes/day)", wings.Name);
        }
    }
}