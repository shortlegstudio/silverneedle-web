// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities.BloodlinePowers
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities.BloodlinePowers;

    public class OneOfUsTests
    {
        private CharacterSheet sorcerer;
        private OneOfUs oneOfUs;
        public OneOfUsTests()
        {
            sorcerer = CharacterTestTemplates.Sorcerer();
            oneOfUs = new OneOfUs();
            sorcerer.Add(oneOfUs);
        }

        [Fact]
        public void GrantsImmunities()
        {
            AssertCharacter.IsImmuneTo("cold", sorcerer);
            AssertCharacter.IsImmuneTo("paralysis", sorcerer);
            AssertCharacter.IsImmuneTo("nonlethal", sorcerer);
            AssertCharacter.IsImmuneTo("sleep", sorcerer);
        }

        [Fact]
        public void GrantsDR()
        {
            AssertCharacter.HasDamageReduction("-", 5,  sorcerer);
        }

        [Fact]
        public void HasBonusVersusUndead()
        {
            Assert.Equal(4, sorcerer.Defense.FortitudeSave.GetConditionalValue("undead spells/abilities"));
        }
    }
}