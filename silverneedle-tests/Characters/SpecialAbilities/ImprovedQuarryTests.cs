// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;

    
    public class ImprovedQuarryTests
    {
        [Fact]
        public void RemovesQuarryAbilityFromCharacter()
        {
            var quarry = new Quarry();
            var impQuarry = new ImprovedQuarry();

            var character = new CharacterSheet();
            character.Add(quarry);
            character.Add(impQuarry);
            Assert.Contains(impQuarry, character.Components.All);
            Assert.DoesNotContain(quarry, character.Components.All);
        }
    }
}