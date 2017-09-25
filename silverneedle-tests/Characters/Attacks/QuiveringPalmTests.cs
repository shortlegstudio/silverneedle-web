// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.Attacks
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Attacks;

    public class QuiveringPalmTests
    {
        [Fact]
        public void SaveDCIsBasedOnLevelAndWisdom()
        {
            var monk = CharacterTestTemplates.MarkyMonk();
            var quiver = new QuiveringPalm();
            monk.Add(quiver);
            monk.AbilityScores.SetScore(AbilityScoreTypes.Wisdom, 16);
            Assert.Equal(13, quiver.SaveDC);
            monk.SetLevel(10);
            Assert.Equal(18, quiver.SaveDC);

        }
    }
}