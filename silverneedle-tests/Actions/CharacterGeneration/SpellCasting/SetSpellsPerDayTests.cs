// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGeneration.SpellCasting
{
    using Xunit;
    using SilverNeedle.Actions.CharacterGeneration.SpellCasting;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Magic;

    
    public class SetSpellsPerDayTests
    {
        SetSpellsPerDay subject;
        
        public SetSpellsPerDayTests()
        {
            subject = new SetSpellsPerDay();
        }

        [Fact]
        public void IfNotACasterDoNothing()
        {
            var character = new CharacterSheet();
            subject.ExecuteStep(character, new CharacterBuildStrategy());
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

            subject.ExecuteStep(character, new CharacterBuildStrategy());

            Assert.Equal(character.Get<SpellCasting>().GetSpellsPerDay(0), 3);
            Assert.Equal(character.Get<SpellCasting>().GetSpellsPerDay(1), 1);
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

            subject.ExecuteStep(character, new CharacterBuildStrategy());

            //Level 0 should not change
            Assert.Equal(character.Get<SpellCasting>().GetSpellsPerDay(0), 3);
            //Level 1 has a bonus
            Assert.Equal(character.Get<SpellCasting>().GetSpellsPerDay(1), 3);
            //Level 2 does not
            Assert.Equal(character.Get<SpellCasting>().GetSpellsPerDay(2), 1);
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

            subject.ExecuteStep(character, new CharacterBuildStrategy());

            //Level 0 should not change
            Assert.Equal(character.Get<SpellCasting>().GetSpellsPerDay(0), 3);
            //Level 1 has a bonus
            Assert.Equal(character.Get<SpellCasting>().GetSpellsPerDay(1), 7);
            //Level 2 does not
            Assert.Equal(character.Get<SpellCasting>().GetSpellsPerDay(2), 5);
        }
    }
}