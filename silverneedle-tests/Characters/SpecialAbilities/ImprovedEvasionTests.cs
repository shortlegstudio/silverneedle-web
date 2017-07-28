// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;

    
    public class ImprovedEvasionTests
    {
        [Fact]
        public void RemovesEvasionAbilityFromCharacter()
        {
            var evasion = new Evasion();
            var impEvasion = new ImprovedEvasion();

            var character = new CharacterSheet();
            character.Add(evasion);
            character.Add(impEvasion);
            Assert.Contains(impEvasion, character.Components.All);
            Assert.DoesNotContain(evasion, character.Components.All);
        }
    }
}