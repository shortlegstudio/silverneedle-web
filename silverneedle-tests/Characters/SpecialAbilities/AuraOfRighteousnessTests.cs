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
    
    
    public class AuraOfRighteousnessTests
    {
        [Fact]
        public void EnablesDamageResistanceAgainstEvil()
        {
            var character = CharacterTestTemplates.AverageBob();
            character.Add(new AuraOfRighteousness());
            var defense = character.Get<DefenseStats>();
            var dr = defense.EnergyResistance.First();
            Assert.Equal(dr.DamageType, "evil");
            Assert.Equal(dr.Amount, 5);
        }

        [Fact]
        public void ProvidesImmunityToCompulsion()
        {
            var character = CharacterTestTemplates.AverageBob();
            character.Add(new AuraOfRighteousness());
            AssertCharacter.IsImmuneTo("Compulsion", character);
        }
    }
}