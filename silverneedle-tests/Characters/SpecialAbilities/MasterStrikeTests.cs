// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using System.Linq;
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;

    
    public class MasterStrikeTests
    {
        [Fact]
        public void AddsSpecialAttackWithDCToSave()
        {
            var character = CharacterTestTemplates.Rogue();
            character.AbilityScores.SetScore(AbilityScoreTypes.Intelligence, 14); //+2
            character.SetLevel(20);
            var ms = new MasterStrike();
            character.Add(ms);
            Assert.Equal(ms.SaveDC, 22);
        }
    }
}