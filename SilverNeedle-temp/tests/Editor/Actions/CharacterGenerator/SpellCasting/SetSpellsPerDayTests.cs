// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGenerator.SpellCasting
{
    using NUnit.Framework;
    using SilverNeedle.Actions.CharacterGenerator.SpellCasting;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Magic;

    [TestFixture]
    public class SetSpellsPerDayTests
    {
        SetSpellsPerDay subject;
        
        [SetUp]
        public void SetUp()
        {
            subject = new SetSpellsPerDay();
        }

        [Fact]
        public void IfNotACasterDoNothing()
        {
            var character = new CharacterSheet();
            subject.Process(character, new CharacterBuildStrategy());
        }

        [Fact]
        public void SetSpellsAvailableForThatDayForFirstLevel()
        {
            var character = new CharacterSheet();
            var cls = new Class();
            cls.Spells.PerDay[1] = new int[] { 3, 1 };
            character.SetClass(cls);
            var spellCasting = new SpellCasting(character.Get<Inventory>(), character.Get<ClassLevel>(), "wizard");
            spellCasting.SetCastingAbility(new AbilityScore(AbilityScoreTypes.Wisdom, 10));
            character.Add(spellCasting);

            subject.Process(character, new CharacterBuildStrategy());

            Assert.That(character.Get<SpellCasting>().GetSpellsPerDay(0), Is.EqualTo(3));
            Assert.That(character.Get<SpellCasting>().GetSpellsPerDay(1), Is.EqualTo(1));
        }

        [Fact]
        public void GrantBonusSpellsPerDayForAbilityScore()
        {
            var character = new CharacterSheet();
            var cls = new Class();
            cls.Spells.PerDay[1] = new int[] { 3, 2, 1 };
            character.SetClass(cls);
            var abilityScore = new AbilityScore(AbilityScoreTypes.Charisma, 13);
            var spellCasting = new SpellCasting(character.Get<Inventory>(), character.Get<ClassLevel>(), "wizard");
            spellCasting.SetCastingAbility(abilityScore);
            character.Add(spellCasting);

            subject.Process(character, new CharacterBuildStrategy());

            //Level 0 should not change
            Assert.That(character.Get<SpellCasting>().GetSpellsPerDay(0), Is.EqualTo(3));
            //Level 1 has a bonus
            Assert.That(character.Get<SpellCasting>().GetSpellsPerDay(1), Is.EqualTo(3));
            //Level 2 does not
            Assert.That(character.Get<SpellCasting>().GetSpellsPerDay(2), Is.EqualTo(1));
        }

        [Fact]
        public void HighAbilityScoresShouldGrantExtraBonusSpells()
        {
            var character = new CharacterSheet();
            var abilityScore = new AbilityScore(AbilityScoreTypes.Charisma, 45);
            var cls = new Class();
            cls.Spells.PerDay[1] = new int[] { 3, 2, 1 };
            character.SetClass(cls);
            var spellCasting = new SpellCasting(character.Get<Inventory>(), character.Get<ClassLevel>(), "wizard");
            spellCasting.SetCastingAbility(abilityScore);
            character.Add(spellCasting);

            subject.Process(character, new CharacterBuildStrategy());

            //Level 0 should not change
            Assert.That(character.Get<SpellCasting>().GetSpellsPerDay(0), Is.EqualTo(3));
            //Level 1 has a bonus
            Assert.That(character.Get<SpellCasting>().GetSpellsPerDay(1), Is.EqualTo(7));
            //Level 2 does not
            Assert.That(character.Get<SpellCasting>().GetSpellsPerDay(2), Is.EqualTo(5));
        }
    }
}