// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities.BloodlinePowers
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities.BloodlinePowers;

    public class DeathsGiftTests
    {
        private CharacterSheet sorcerer;
        private DeathsGift gift;

        public DeathsGiftTests()
        {
            sorcerer = CharacterTestTemplates.Sorcerer();
            gift = new DeathsGift();
            sorcerer.Add(gift);
        }

        [Fact]
        public void GrantsResistanceToColdAndNonLethalDamage()
        {
            AssertCharacter.HasDamageResistance("- against nonlethal", 5, sorcerer);
            AssertCharacter.HasResistanceTo("cold", 5, sorcerer);
            sorcerer.SetLevel(9);
            gift.LeveledUp(sorcerer.Components);
            AssertCharacter.HasDamageResistance("- against nonlethal", 10, sorcerer);
            AssertCharacter.HasResistanceTo("cold", 10, sorcerer);
        }
    }
}