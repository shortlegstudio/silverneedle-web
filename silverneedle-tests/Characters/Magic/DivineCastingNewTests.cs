// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.Magic
{
    using System.Linq;
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Magic;

    public class DivineCastingNewTests
    {
        [Fact]
        public void DivineCastersKnowAllSpellsAvailableAtTime()
        {
            var cleric = CharacterTestTemplates.Cleric().WithDivineCasting();
            var casting = cleric.Get<DivineCastingNew>();
            var spellList = casting.SpellList;
            AssertCharacter.KnowsSpells(0, spellList.GetSpells(0), cleric);
            AssertCharacter.KnowsSpells(1, spellList.GetSpells(1), cleric);

        }

        [Fact]
        public void CanReadySpellsFromListOfAvailableSpells()
        {
            var cleric = CharacterTestTemplates.Cleric().WithDivineCasting();
            var casting = cleric.Get<DivineCastingNew>();
            var spellList = casting.SpellList;
            casting.PrepareSpell(0, "spell 0-1");
            casting.PrepareSpell(1, "spell 1-1");
            AssertExtensions.EquivalentLists(new string[] { "spell 0-1" }, casting.GetReadySpells(0));
            AssertExtensions.EquivalentLists(new string[] { "spell 1-1" }, casting.GetReadySpells(1));
        }

        [Fact]
        public void GetReadySpellsReturnsEmptyListIfNoSpellsAreReady()
        {
            var cleric = CharacterTestTemplates.Cleric().WithDivineCasting();
            var casting = cleric.Get<DivineCastingNew>();
            AssertExtensions.EquivalentLists(new string[] { }, casting.GetReadySpells(6));
        }
    }
}