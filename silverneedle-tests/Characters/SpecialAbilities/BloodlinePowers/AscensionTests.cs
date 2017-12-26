// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities.BloodlinePowers
{
    using System.Linq;
    using Xunit;
    using SilverNeedle.Characters.SpecialAbilities.BloodlinePowers;

    public class AscensionTests
    {
        [Fact]
        public void AddsDRAndImmunities()
        {
            var sorcerer = CharacterTestTemplates.Sorcerer();
            var ascension = new Ascension();
            sorcerer.Add(ascension);
            Assert.True(
                sorcerer.Defense.EnergyResistance.Any(dr =>
                dr.DamageType == "electricity" && dr.Amount == 10)
            );
            Assert.True(
                sorcerer.Defense.EnergyResistance.Any(dr =>
                dr.DamageType == "fire" && dr.Amount == 10)
            );

            AssertCharacter.IsImmuneTo("acid", sorcerer);
            AssertCharacter.IsImmuneTo("cold", sorcerer);
            AssertCharacter.IsImmuneTo("petrification", sorcerer);
            
            Assert.Equal(4, sorcerer.Defense.FortitudeSave.GetConditionalValue("poison"));
        }
    }

}