// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using Xunit;
    using SilverNeedle.Characters.SpecialAbilities;

    public class ForewarnedTests 
    {
        [Fact]
        public void ProvidesABonusToInitiative()
        {
            var character = CharacterTestTemplates.Wizard();
            var forewarned = new Forewarned();
            character.Add(forewarned);
            Assert.Equal(1, character.Initiative.TotalValue);
            character.SetLevel(6);

            Assert.Equal(3, character.Initiative.TotalValue);
        }
    }
}