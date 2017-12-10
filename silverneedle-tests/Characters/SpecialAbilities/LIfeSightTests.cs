// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using Xunit;
    using SilverNeedle.Characters.SpecialAbilities;

    public class LifeSightTests
    {
        [Fact]
        public void SightRangeIsDeterminedByLevel()
        {
            var wizard = CharacterTestTemplates.Wizard();
            wizard.SetLevel(8);
            var lifeSight = new LifeSight();
            wizard.Add(lifeSight);
            Assert.Equal(10, lifeSight.Range);
            wizard.SetLevel(12);
            Assert.Equal(20, lifeSight.Range);
            wizard.SetLevel(16);
            Assert.Equal(30, lifeSight.Range);
            wizard.SetLevel(20);
            Assert.Equal(40, lifeSight.Range);

            Assert.Equal(20, lifeSight.RoundsPerDay);

            Assert.Equal("Life Sight 40ft (20 rounds/day)", lifeSight.Name);

        }
    }
}