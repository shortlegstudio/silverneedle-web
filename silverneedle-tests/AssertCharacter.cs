// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests
{
    using System.Linq;
    using Xunit;
    using SilverNeedle.Characters;
    public static class AssertCharacter
    {
        public static void HasDamageResistance(CharacterSheet character, string damageType, int amount)
        {
            HasDamageResistance(damageType, amount, character);
        }

        public static void HasDamageResistance(string damageType, int amount, CharacterSheet character)
        {
            Assert.True(character.Defense.DamageResistance.Any(
                dr => dr.DamageType == damageType &&
                    dr.Amount == amount
                ), string.Format("Expected damage resistance {0}/{1}", amount, damageType)
            );
        }

        public static void HasResistanceTo(string damageType, int amount, CharacterSheet character)
        {
            Assert.True(character.Defense.DamageResistance.Any(
                dr => dr.DamageType == damageType &&
                    dr.Amount == amount
                ), string.Format("Expected damage resistance {0}/{1}", amount, damageType)
            );
        }

        public static void IsImmuneTo(string damageType, CharacterSheet character)
        {
            Assert.True(
                character.Defense.Immunities.Any(x =>
                x.Condition == damageType),
                string.Format("Expected immunity to {0}", damageType)
            );
        }
    }
}