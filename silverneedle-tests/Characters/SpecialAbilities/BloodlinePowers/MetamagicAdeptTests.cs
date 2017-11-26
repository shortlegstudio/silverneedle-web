// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities.BloodlinePowers
{
    using Xunit;
    using SilverNeedle.Characters.SpecialAbilities.BloodlinePowers;

    public class MetamagicAdeptTests
    {
        [Fact]
        public void IncreasesUsesPerDayDependingOnSorcererLevel()
        {
            var sorcerer = CharacterTestTemplates.Sorcerer();
            var metamagic = new MetamagicAdept();
            sorcerer.Add(metamagic);
            sorcerer.SetLevel(3);
            Assert.Equal(1, metamagic.UsesPerDay);

            sorcerer.SetLevel(7);
            Assert.Equal(2, metamagic.UsesPerDay);
            sorcerer.SetLevel(11);
            Assert.Equal(3, metamagic.UsesPerDay);
            sorcerer.SetLevel(15);
            Assert.Equal(4, metamagic.UsesPerDay);
        }
    }
}