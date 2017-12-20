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
    
    
    public class AuraOfCourageTests
    {
        [Fact]
        public void AuraOfCourageEnablesImmunityToFear()
        {
            var character = CharacterTestTemplates.AverageBob();
            character.Add(new AuraOfCourage());
            AssertCharacter.IsImmuneTo("Fear", character);
        }
    }
}