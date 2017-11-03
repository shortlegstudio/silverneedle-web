// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.Magic
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Magic;
    using SilverNeedle.Serialization;
    using SilverNeedle.Spells;
    using SilverNeedle.Utility;

    public class SpontaneousCastingTests
    {
        [Fact]
        public void LoadsDetailsAboutHowTheSpellsAreManaged()
        {
            var bard = CharacterTestTemplates.BardyBard();
            var spellCasting = new SpontaneousCasting(configuration);
            bard.Add(spellCasting);
            Assert.Equal("bard", spellCasting.SpellList);
            Assert.Equal(SpellType.Arcane, spellCasting.SpellType);
            Assert.Equal(bard.AbilityScores.GetAbility(AbilityScoreTypes.Charisma), spellCasting.CastingAbility);
            Assert.Equal(SpellsKnown.Spontaneous, spellCasting.SpellsKnown);
            Assert.Equal(4, spellCasting.GetSpellsPerDay(0));
            Assert.Equal(1, spellCasting.GetSpellsPerDay(1));
            bard.SetLevel(2);
            Assert.Equal(5, spellCasting.GetSpellsPerDay(0));
            Assert.Equal(2, spellCasting.GetSpellsPerDay(1));
            bard.SetLevel(3);
            Assert.Equal(6, spellCasting.GetSpellsPerDay(0));
            Assert.Equal(3, spellCasting.GetSpellsPerDay(1));
            Assert.Equal(bard.Class, spellCasting.Class.Class);
            Assert.Equal(bard.Level, spellCasting.CasterLevel);
        }

        [Fact]
        public void HighCharismaImprovesSpellsPerDay()
        {
            var bard = CharacterTestTemplates.BardyBard();
            var spellCasting = new SpontaneousCasting(configuration);
            bard.Add(spellCasting);
            bard.AbilityScores.SetScore(AbilityScoreTypes.Charisma, 16);
            Assert.Equal(2, spellCasting.GetSpellsPerDay(1));
        }

        [Fact]
        public void CharismaBonusMaxesOutDependingOnModifier()
        {
            var bard = CharacterTestTemplates.BardyBard();
            var spellCasting = new SpontaneousCasting(configuration);
            bard.Add(spellCasting);
            bard.SetLevel(3);
            bard.AbilityScores.SetScore(AbilityScoreTypes.Charisma, 12);
            Assert.Equal(4, spellCasting.GetSpellsPerDay(1));
            Assert.Equal(2, spellCasting.GetSpellsPerDay(2));
        }
        IObjectStore configuration = @"
list: bard
type: arcane
casting-ability: charisma
spell-slots:
  1: [4, 1]
  2: [5, 2]
  3: [6, 3, 2]
spells-known:
  1: [4, 2]
  2: [5, 3]
  3: [6, 4]
".ParseYaml();
    }
}