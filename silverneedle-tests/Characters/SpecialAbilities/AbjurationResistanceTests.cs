// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using System.Linq;
    using Xunit;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Core;
    using SilverNeedle.Serialization;

    public class AbjurationResistanceTests
    {
        [Fact]
        public void SelectsAnEnergyTypeToGenerateResistanceTo()
        {
            var wizard = CharacterTestTemplates.Wizard();
            var energyType = new EnergyType();
            energyType.Name = "fire"; 
            var abjRes = new AbjurationResistance(EntityGateway<EnergyType>.LoadWithSingleItem(energyType));
            wizard.Add(abjRes);
            AssertCharacter.HasDamageResistance("fire", 5, wizard);

            wizard.SetLevel(11);
            AssertCharacter.HasDamageResistance("fire", 10, wizard);

            wizard.SetLevel(20);
            AssertCharacter.IsImmuneTo("fire", wizard);
        }
    }
}