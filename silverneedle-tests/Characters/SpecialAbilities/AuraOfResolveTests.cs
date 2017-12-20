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
    
    
    public class AuraOfResolveTests
    {
        [Fact]
        public void AuraOfResolveEnablesImmunityToCharms()
        {
            var character = CharacterTestTemplates.AverageBob();
            character.Add(new AuraOfResolve());
            var defense = character.Get<DefenseStats>();
            AssertCharacter.IsImmuneTo("Charms", character);
        }
    }
}