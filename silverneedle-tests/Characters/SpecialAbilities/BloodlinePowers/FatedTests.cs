// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities.BloodlinePowers
{
    using Xunit;
    using SilverNeedle.Characters.SpecialAbilities.BloodlinePowers;

    public class FatedTests
    {
        [Fact]
        public void GainBonusOnDependingOnLevel()
        {
            var sorcerer = CharacterTestTemplates.Sorcerer();
            var fated = new Fated();
            sorcerer.Add(fated);
            sorcerer.SetLevel(3);
            Assert.Equal(1, fated.Bonus);
            sorcerer.SetLevel(7);
            Assert.Equal(2, fated.Bonus);
            sorcerer.SetLevel(11);
            Assert.Equal(3, fated.Bonus);
            sorcerer.SetLevel(19);
            Assert.Equal(5, fated.Bonus);
        }
    }
}