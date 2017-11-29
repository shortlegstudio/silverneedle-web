// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities.BloodlinePowers
{
    using Xunit;
    using SilverNeedle.Characters.SpecialAbilities.BloodlinePowers;

    public class ElementalBodyTests
    {
        [Fact]
        public void GrantsImmunitiesBasedOnElementType()
        {
            var sorcerer = CharacterTestTemplates.Sorcerer();
            var elementType = new ElementalType();
            elementType.EnergyType = "acid";
            sorcerer.Add(elementType);
            sorcerer.Add(new ElementalBody());

            AssertCharacter.IsImmuneTo("acid", sorcerer);
            AssertCharacter.IsImmuneTo("sneak attacks", sorcerer);
            AssertCharacter.IsImmuneTo("critical hits", sorcerer);
        }
    }

}