// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities.BloodlinePowers
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities.BloodlinePowers;

    public class SoulOfTheFeyTests
    {
        [Fact]
        public void ProvidesImmunities()
        {
            var sorcerer = CharacterTestTemplates.Sorcerer();
            sorcerer.SetLevel(20);
            sorcerer.Add(new SoulOfTheFey());
            AssertCharacter.IsImmuneTo("poison", sorcerer);
            AssertCharacter.HasDamageResistance(sorcerer, "cold iron", 10);
        }
    }
}