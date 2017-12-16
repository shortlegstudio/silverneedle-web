// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.Magic
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Magic;
    using SilverNeedle.Spells;

    public class CannotCastSpellsOfOpposingAlignmentTests
    {
        [Fact]
        public void SpellsWithADescriptorOfOpposingAlignmentAreNotInAvailableList()
        {
            var caster = CharacterTestTemplates.Cleric();
            caster.Alignment = CharacterAlignment.LawfulEvil;
            var rule = new CannotCastSpellsOfOpposingAlignment();
            caster.Add(rule);

            var spell = new Spell("Good One", "healing", new string[] { "good" });
            var evilSpell = new Spell("Good One", "healing", new string[] { "evil" });
            Assert.False(rule.CanCastSpell(spell));
            Assert.True(rule.CanCastSpell(evilSpell));
        }
    }
}