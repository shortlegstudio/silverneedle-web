// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.Magic
{
    using NUnit.Framework;
    using SilverNeedle;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Magic;
    using SilverNeedle.Equipment;
    using SilverNeedle.Spells;
    using SilverNeedle.Utility;

    [TestFixture]
    public class SpellCastingTests
    {
        [Test]
        public void TracksAvailableSpellsForTheCharacter()
        {
            var cls = new ClassLevel(new Class());
            var spellcasting = new SpellCasting(new Inventory(), cls, "wizard");
            spellcasting.AddSpells(0, new Spell[] { new Spell("cantrip1", "evocation"), new Spell("cantrip2", "evocation") });
            Assert.That(spellcasting.GetAvailableSpells(0), Is.EquivalentTo(new string[] { "cantrip1", "cantrip2" }));
        }

        [Test]
        public void IfDependentOnSpellbookPullsAvailableFromSpellbook()
        {
            var inventory = new Inventory();
            var cls = new ClassLevel(new Class());
            var spellcasting = new SpellCasting(inventory, cls, "wizard");
            spellcasting.SpellsKnown = SpellsKnown.Spellbook;

            // Make a spellbook
            var spellbook = new Spellbook();
            spellbook.AddSpells(0, new string[] { "cantrip1", "cantrip2" });
            inventory.AddGear(spellbook);

            Assert.That(spellcasting.GetAvailableSpells(0), Is.EquivalentTo(new string[] { "cantrip1", "cantrip2" }));
        }

        [Test]
        public void MaxLevelIsSetForNowByTheMaxLevelKnown()
        {
            var cls = new ClassLevel(new Class());
            var spells = new SpellCasting(new Inventory(), cls, "wizard"); 
            spells.SpellsKnown = SpellsKnown.All;
            spells.AddSpells(0, new Spell[] { new Spell("foo", "bar") });
            spells.AddSpells(1, new Spell[] { new Spell("foo", "bar") });
            spells.AddSpells(2, new Spell[] { new Spell("foo", "bar") });

            Assert.That(spells.MaxLevel, Is.EqualTo(2));
        }

        [Test]
        public void CanSpecifyTheNumberOfSpellsPerDay()
        {
            var cls = new ClassLevel(new Class());
            var spells = new SpellCasting(new Inventory(), cls, "wizard");
            spells.SetSpellsPerDay(0, 3);
            spells.SetSpellsPerDay(1, 1);

            Assert.That(spells.GetSpellsPerDay(0), Is.EqualTo(3));
            Assert.That(spells.GetSpellsPerDay(1), Is.EqualTo(1));
            Assert.That(spells.GetSpellsPerDay(2), Is.EqualTo(0));
        }

        [Test]
        public void SpellsCanBePrepared()
        {
            var cls = new ClassLevel(new Class());
            var spells = new SpellCasting(new Inventory(), cls, "wizard");
            spells.AddSpells(0, new Spell[] { new Spell("cantrip1", "evocation"), new Spell("cantrip2", "evocation") });
            spells.PrepareSpells(0, new string[] { "cantrip1" });
            Assert.That(spells.GetPreparedSpells(0), Is.EquivalentTo(new string[] {"cantrip1"}));
        }

        [Test]
        public void CalculatesTheDCBasedOnSpellLevelAndAbility()
        {
            var cls = new ClassLevel(new Class());
            var spells = new SpellCasting(new Inventory(), cls, "wizard");
            var abilityScore = new AbilityScore(AbilityScoreTypes.Intelligence, 18);
            spells.SetCastingAbility(abilityScore);
            Assert.That(spells.GetDifficultyClass(0), Is.EqualTo(14));
            Assert.That(spells.GetDifficultyClass(3), Is.EqualTo(17));
            Assert.That(spells.GetDifficultyClass(9), Is.EqualTo(23));
        }

        [Test]
        public void IfAskedForSpellsPastMaxLevelJustReturnZero()
        {
            var cls = new ClassLevel(new Class());
            var spells = new SpellCasting(new Inventory(), cls, "wizard");
            Assert.That(spells.GetSpellsPerDay(200), Is.EqualTo(0));

        }

        [Test]
        public void RulesCanBeAppliedToSpellCastingThatLimitsAvailableSpells()
        {
            var cls = new ClassLevel(new Class());
            var spellcasting = new SpellCasting(new Inventory(), cls, "wizard");
            spellcasting.AddSpells(0, new Spell[] { new Spell("cantrip1", "conjuration"), new Spell("cantrip2", "evocation") });
            var cannotCastConjuration = new CannotCastConjuration();
            spellcasting.AddRule(cannotCastConjuration);
            Assert.That(spellcasting.GetAvailableSpells(0), Is.EquivalentTo(new string[] { "cantrip2" }));
        }

        [Test]
        public void IfTryingToPrepareSpellsAndThereAreNoKnownSpellsDoNothing()
        {
            var sc = new SpellCasting(new Inventory(), new ClassLevel(new Class()), "wizard");
            Assert.DoesNotThrow(() => {sc.PrepareSpells(0, new string[] { }); });
        }

        public class CannotCastConjuration : ISpellCastingRule
        {
            public bool CanCastSpell(Spell spell)
            {
                return !spell.School.EqualsIgnoreCase("conjuration");
            }
        }
    }
}