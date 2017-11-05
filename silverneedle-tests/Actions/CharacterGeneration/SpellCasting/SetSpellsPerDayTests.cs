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
            var character = CharacterTestTemplates.AverageBob();
            subject.ExecuteStep(character);
        }

        [Fact]
        public void SetSpellsAvailableForThatDayForFirstLevel()
        {
            var character = CharacterTestTemplates.AverageBob();
            var cls = new Class();
            cls.Spells.PerDay[1] = new int[] { 3, 1 };
            character.SetClass(cls);
            var spellCasting = new DivineCasting(character.Get<ClassLevel>(), "wizard");
            spellCasting.SetCastingAbility(new AbilityScore(AbilityScoreTypes.Wisdom, 10));
            character.Add(spellCasting);

            subject.ExecuteStep(character);

            Assert.Equal(3, spellCasting.GetSpellsPerDay(0));
            Assert.Equal(1, spellCasting.GetSpellsPerDay(1));
        }

        [Fact]
        public void GrantBonusSpellsPerDayForAbilityScore()
        {
            var character = CharacterTestTemplates.AverageBob();
            var cls = new Class();
            cls.Spells.PerDay[1] = new int[] { 3, 2, 1 };
            character.SetClass(cls);
            var abilityScore = new AbilityScore(AbilityScoreTypes.Charisma, 13);
            var spellCasting = new DivineCasting(character.Get<ClassLevel>(), "wizard");
            spellCasting.SetCastingAbility(abilityScore);
            character.Add(spellCasting);

            subject.ExecuteStep(character);

            //Level 0 should not change
            Assert.Equal(3, spellCasting.GetSpellsPerDay(0));
            //Level 1 has a bonus
            Assert.Equal(3, spellCasting.GetSpellsPerDay(1));
            //Level 2 does not
            Assert.Equal(1, spellCasting.GetSpellsPerDay(2));
        }

        [Fact]
        public void HighAbilityScoresShouldGrantExtraBonusSpells()
        {
            var character = CharacterTestTemplates.AverageBob();
            var abilityScore = new AbilityScore(AbilityScoreTypes.Charisma, 45);
            var cls = new Class();
            cls.Spells.PerDay[1] = new int[] { 3, 2, 1 };
            character.SetClass(cls);
            var spellCasting = new DivineCasting(character.Get<ClassLevel>(), "wizard");
            spellCasting.SetCastingAbility(abilityScore);
            character.Add(spellCasting);

            subject.ExecuteStep(character);

            //Level 0 should not change
            Assert.Equal(3, spellCasting.GetSpellsPerDay(0));
            //Level 1 has a bonus
            Assert.Equal(7, spellCasting.GetSpellsPerDay(1));
            //Level 2 does not
            Assert.Equal(5, spellCasting.GetSpellsPerDay(2));
        }
    }
}