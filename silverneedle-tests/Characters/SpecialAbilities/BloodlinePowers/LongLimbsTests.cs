// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities.BloodlinePowers
{
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities.BloodlinePowers;
    using Xunit;

    public class LongLimbsTests 
    {
        [Fact]
        public void LongLimbsAddsReachAndIncreasesWithLevels()
        {
            var sorcerer = CharacterTestTemplates.Sorcerer();
            var longLimbs = new LongLimbs();
            sorcerer.Add(longLimbs);
            sorcerer.SetLevel(3);
            Assert.Equal("Long Limbs (5 ft)", longLimbs.DisplayString());
            sorcerer.SetLevel(11);
            Assert.Equal("Long Limbs (10 ft)", longLimbs.DisplayString());
            sorcerer.SetLevel(17);
            Assert.Equal("Long Limbs (15 ft)", longLimbs.DisplayString());
        }
    }

}