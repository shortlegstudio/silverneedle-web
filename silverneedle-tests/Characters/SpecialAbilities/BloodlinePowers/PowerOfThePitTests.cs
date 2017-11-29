// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities.BloodlinePowers
{
    using Xunit;
    using SilverNeedle.Characters.SpecialAbilities.BloodlinePowers;

    public class PowerOfThePitTests
    {
        [Fact]
        public void ProvidesImmunities()
        {
            var sorcerer = CharacterTestTemplates.Sorcerer();
            var power = new PowerOfThePit();
            sorcerer.Add(power);

            AssertCharacter.HasDamageResistance(sorcerer, "acid", 10);
            AssertCharacter.HasDamageResistance(sorcerer, "cold", 10);
            AssertCharacter.IsImmuneTo("fire", sorcerer);
            AssertCharacter.IsImmuneTo("poison", sorcerer);

        }
    }
}