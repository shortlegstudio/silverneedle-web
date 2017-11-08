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
        CharacterSheet bard;
        SpellList spellList;
        SpontaneousCasting spellCasting;
        public SpontaneousCastingTests()
        {
            spellList = new SpellList();
            spellList.Class = "bard";
            spellList.Add(0, "cantrip1");
            spellList.Add(0, "cantrip2");
            spellList.Add(1, "level1.1");
            spellList.Add(1, "level1.2");
            spellList.Add(2, "level2.1");
            spellList.Add(2, "level2.2");
            bard = CharacterTestTemplates.BardyBard();
            spellCasting = new SpontaneousCasting(configuration, EntityGateway<SpellList>.LoadWithSingleItem(spellList));
            bard.Add(spellCasting);
        }

        [Fact]
        public void SpontaneousCastersHaveLimitedKnownSpells()
        {
            Assert.Equal(4, spellCasting.GetKnownSpellCount(0));
            Assert.Equal(2, spellCasting.GetKnownSpellCount(1));
            bard.SetLevel(3);

            Assert.Equal(6, spellCasting.GetKnownSpellCount(0));
            Assert.Equal(4, spellCasting.GetKnownSpellCount(1));
            Assert.Equal(1, spellCasting.GetKnownSpellCount(2));
        }

        [Fact]
        public void AskingForKnownSpellsAtALevelUnknownJustReturnsZero()
        {
            Assert.Equal(0, spellCasting.GetKnownSpellCount(6));
        }

        [Fact]
        public void SpontaneousCastersNeedToBeToldWhatSpellsTheyKnow()
        {
            spellCasting.LearnSpell("cantrip1");
            spellCasting.LearnSpell("cantrip2");
            spellCasting.LearnSpell("level1.1");
            Assert.Equal(new string[] { "cantrip1", "cantrip2" }, spellCasting.GetReadySpells(0));
            Assert.Equal(new string[] { "level1.1" }, spellCasting.GetReadySpells(1));
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
  3: [6, 4, 1]
".ParseYaml();
    }
}