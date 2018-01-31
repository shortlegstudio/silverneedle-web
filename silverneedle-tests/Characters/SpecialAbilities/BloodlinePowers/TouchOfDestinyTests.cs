// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities.BloodlinePowers
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities.BloodlinePowers;

    public class TouchOfDestinyTests
    {
        [Fact]
        public void GrantsBonusToCharactersEqualToHalfSorcererLevels()
        {
            var sorcerer = CharacterTestTemplates.Sorcerer();
            var touch = new TouchOfDestiny();
            sorcerer.Add(touch);

            Assert.Equal(3, touch.UsesPerDay);
            sorcerer.AbilityScores.SetScore(AbilityScoreTypes.Charisma, 16);
            Assert.Equal(6, touch.UsesPerDay);

            Assert.Equal(1, touch.Bonus);
            sorcerer.SetLevel(10);
            Assert.Equal(5, touch.Bonus);
            Assert.Equal("Touch Of Destiny (+5 bonus 6/day)", touch.DisplayString());
        }
    }
}