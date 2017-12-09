// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;

    public class DivinersFortuneTests
    {
        [Fact]
        public void GrantsBonusBasedOnCharacterLevel()
        {
            var wizard = CharacterTestTemplates.Wizard();
            var div = new DivinersFortune();
            wizard.Add(div);
            Assert.Equal(1, div.Bonus);
            wizard.SetLevel(10);
            Assert.Equal(5, div.Bonus);
            Assert.Equal(3, div.UsesPerDay);
            wizard.AbilityScores.SetScore(AbilityScoreTypes.Intelligence, 16);
            Assert.Equal(6, div.UsesPerDay);
        }
    }
}