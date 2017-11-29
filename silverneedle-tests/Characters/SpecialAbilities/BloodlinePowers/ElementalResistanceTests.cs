// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities.BloodlinePowers
{
    using System.Linq;
    using Xunit;
    using SilverNeedle.Characters.SpecialAbilities.BloodlinePowers;

    public class ElementalResistanceTests
    {
        [Fact]
        public void GrantsResistanceBasedOnEnergyType()
        {
            var sorcerer = CharacterTestTemplates.Sorcerer();
            var elementalType = new ElementalType();
            elementalType.EnergyType = "cold";
            sorcerer.Add(elementalType);
            var resist = new ElementalResistance();
            sorcerer.Add(resist);

            AssertCharacter.HasDamageResistance(sorcerer, "cold", 10);
            sorcerer.SetLevel(9);
            resist.LeveledUp(sorcerer.Components);
            AssertCharacter.HasDamageResistance(sorcerer, "cold", 20);

        }
    }
}