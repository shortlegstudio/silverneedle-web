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
    
    
    public class DivineHealthTests
    {
        [Fact]
        public void EnablesImmunityToDisease()
        {
            var character = CharacterTestTemplates.AverageBob();
            character.Add(new DivineHealth());
            var defense = character.Get<DefenseStats>();
            AssertCharacter.IsImmuneTo("Disease", character);
        }
    }
}