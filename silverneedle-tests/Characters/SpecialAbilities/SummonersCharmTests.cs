// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using Xunit;
    using SilverNeedle.Characters.SpecialAbilities;

    public class SummonersCharmTests
    {
        [Fact]
        public void ProvideExtraRoundsEqualToHalfLevelsMinimumOne()
        {
            var wizard = CharacterTestTemplates.Wizard();
            var summon = new SummonersCharm();
            wizard.Add(summon);
            Assert.Equal(1, summon.Duration);
            wizard.SetLevel(10);
            Assert.Equal(5, summon.Duration);
            Assert.Equal("Summoners Charm (5 rounds)", summon.DisplayString());
        }
    }
}