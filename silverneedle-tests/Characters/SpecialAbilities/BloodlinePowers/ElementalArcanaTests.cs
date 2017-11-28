// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities.BloodlinePowers
{
    using Xunit;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Characters.SpecialAbilities.BloodlinePowers;
    public class ElementalArcanaTests
    {
        [Fact]
        public void CanChangeEnergyDamageToTypeOfElemental()
        {
            var sorcerer = CharacterTestTemplates.Sorcerer();
            var elementalType = new ElementalType();
            elementalType.EnergyType = "cold";
            sorcerer.Add(elementalType);

            var arcana = new ElementalArcana();
            sorcerer.Add(arcana);

            Assert.Equal("change energy damage spells to cold", arcana.BonusAbility);
        }
    }
}