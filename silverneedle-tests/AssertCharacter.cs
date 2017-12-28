// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests
{
    using System.Linq;
    using System.Collections.Generic;
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Magic;

    public static class AssertCharacter
    {
        public static void HasDamageResistance(CharacterSheet character, string damageType, int amount)
        {
            HasDamageResistance(damageType, amount, character);
        }

        public static void HasDamageResistance(string damageType, int amount, CharacterSheet character)
        {
            Assert.True(character.Defense.EnergyResistance.Any(
                dr => dr.DamageType == damageType &&
                    dr.Amount == amount
                ), string.Format("Expected damage resistance {0}/{1}", amount, damageType)
            );
        }

        public static void HasDamageReduction(string bypassType, int amount, CharacterSheet character)
        {
            Assert.True(character.Defense.DamageReduction.Any(
                dr => dr.BypassType == bypassType && dr.TotalValue == amount
            ), string.Format("Expected damage reduction {0}/{1}", amount, bypassType));
        }

        public static void HasResistanceTo(string damageType, int amount, CharacterSheet character)
        {
            Assert.True(character.Defense.EnergyResistance.Any(
                dr => dr.DamageType == damageType &&
                    dr.Amount == amount
                ), string.Format("Expected damage resistance {0}/{1}", amount, damageType)
            );
        }

        public static void IsImmuneTo(string damageType, CharacterSheet character)
        {
            Assert.True(
                character.Defense.Immunities.Any(x =>
                x.DamageType == damageType),
                string.Format("Expected immunity to {0}", damageType)
            );
        }

        public static void IsCarrying<T>(CharacterSheet character)
        {
            Assert.True(
                character.Inventory.GearOfType<T>().Count() > 0,
                string.Format("Expected to be carrying a {0}", typeof(T))
            );
        }

        public static void HasFeatToken(string tokenTag, CharacterSheet character)
        {
            var tokens = character.FeatTokens;
            Assert.True(
                tokens.Any(x => x.Tags.Contains(tokenTag))
            );
        }

        public static void KnowsSpells(int spellLevel, IEnumerable<string> spells, CharacterSheet character)
        {
            var casting = character.Get<ISpellCasting>();
            var knownSpells = casting.GetKnownSpells(spellLevel);
            AssertExtensions.EquivalentLists(spells, knownSpells);
        }

        public static void HasClassSkill(string className, CharacterSheet character)
        {
            Assert.True(character.SkillRanks.GetClassSkills().Any(x => x.Matches(className)));
        }
    }
}