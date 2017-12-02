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

    public class SpellCastingTests
    {
        SpellList spellList;
        CharacterSheet bard;
        SpellCasting spellCasting;
        public SpellCastingTests()
        {
            spellList = new SpellList();
            spellList.Class = "bard";
            spellList.Add(1, "magic missile");
            var gateway = EntityGateway<SpellList>.LoadWithSingleItem(spellList);
            bard = CharacterTestTemplates.BardyBard();
            spellCasting = new SpellCasting(configuration, gateway);
            bard.Add(spellCasting);
        }
        [Fact]
        public void LoadsDetailsAboutHowTheSpellsAreManaged()
        {
            Assert.Equal("bard", spellCasting.SpellListName);
            Assert.Equal(SpellType.Arcane, spellCasting.SpellType);
            Assert.Equal(bard.AbilityScores.GetAbility(AbilityScoreTypes.Charisma), spellCasting.CastingAbility);
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
            bard.AbilityScores.SetScore(AbilityScoreTypes.Charisma, 16);
            Assert.Equal(2, spellCasting.GetSpellsPerDay(1));
        }

        [Fact]
        public void CharismaBonusMaxesOutDependingOnModifier()
        {
            bard.SetLevel(3);
            bard.AbilityScores.SetScore(AbilityScoreTypes.Charisma, 12);
            Assert.Equal(4, spellCasting.GetSpellsPerDay(1));
            Assert.Equal(2, spellCasting.GetSpellsPerDay(2));
        }

        [Fact]
        public void LoadsTheSpellList()
        {
            Assert.Equal(spellList, spellCasting.SpellList);
        }

        [Fact]
        public void DifficultyClassIsDependentOnLevelAndCastingAbility()
        {
            bard.AbilityScores.SetScore(AbilityScoreTypes.Charisma, 16);
            Assert.Equal(13, spellCasting.GetDifficultyClass(0));
            Assert.Equal(14, spellCasting.GetDifficultyClass(1));

        }

        [Fact]
        public void IfNoSlotsForLevelJustReturnZero()
        {
            Assert.Equal(0, spellCasting.GetSpellsPerDay(16));
        }

        [Fact]
        public void HighestSpellLevelKnownCalculatedFromWhatSpellSlotsAreAvailable()
        {
            Assert.Equal(1, spellCasting.GetHighestSpellLevelKnown());
            bard.SetLevel(3);

            Assert.Equal(2, spellCasting.GetHighestSpellLevelKnown());
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