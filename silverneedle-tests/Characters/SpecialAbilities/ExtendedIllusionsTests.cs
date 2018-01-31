// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using Xunit;
    using SilverNeedle.Characters.SpecialAbilities;

    public class ExtendedIllusionsTests
    {
        [Fact]
        public void ExtraRoundsBasedOnSourceLevel()
        {
            var wizard = CharacterTestTemplates.Wizard();
            var extend = new ExtendedIllusions();
            wizard.Add(extend);
            Assert.Equal(1, extend.Duration);
            wizard.SetLevel(10);
            Assert.Equal(5, extend.Duration);
            Assert.Equal("Extended Illusions (5 rounds)", extend.DisplayString());
        }
    }
}