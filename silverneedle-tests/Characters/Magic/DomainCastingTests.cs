// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.Magic
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Magic;
    using SilverNeedle.Characters.Domains;
    using SilverNeedle.Serialization;

    public class DomainCastingTests
    {
        private CharacterSheet cleric;
        private DomainCasting domainCasting;
        public DomainCastingTests()
        {
            cleric = CharacterTestTemplates.Cleric();
            var domain = Domain.CreateForTesting("air", new string[] { "air-1", "air-2" });
            var domain2 = Domain.CreateForTesting("earth", new string[] { "earth-1", "earth-2" });
            var config = new MemoryStore();
            config.SetValue("casting-ability", "wisdom");
            domainCasting = new DomainCasting(config);
            cleric.Add(domain);
            cleric.Add(domain2);
            cleric.Add(domainCasting);
        }
        [Fact]
        public void DomainCastingLoadsSpellListsFromMultipleDomains()
        {
            AssertExtensions.EquivalentLists(new string[] { "air-1", "earth-1" }, domainCasting.GetKnownSpells(1));
            Assert.Equal(0, domainCasting.GetSpellsPerDay(0));
            Assert.Equal(1, domainCasting.GetSpellsPerDay(1));
        }

        [Fact]
        public void SpellSlotsAreCalculatedBasedOnCharacterLevel()
        {
            Assert.Equal(1, domainCasting.GetHighestSpellLevelKnown());
            Assert.Equal(0, domainCasting.GetSpellsPerDay(0));
            Assert.Equal(1, domainCasting.GetSpellsPerDay(1));
            cleric.SetLevel(11);
            Assert.Equal(6, domainCasting.GetHighestSpellLevelKnown());
            Assert.Equal(1, domainCasting.GetSpellsPerDay(6));
        }

        [Fact]
        public void CanPrepareASpellForALevel()
        {
            AssertExtensions.EquivalentLists(new string[] { }, domainCasting.GetReadySpells(1));
            domainCasting.PrepareSpell(1, "air-1");
            AssertExtensions.EquivalentLists(new string[] { "air-1" }, domainCasting.GetReadySpells(1));
        }

        [Fact]
        public void SpellListNameContainsDomainNames()
        {
            Assert.Equal("Domain (air, earth)", domainCasting.SpellListName);
        }

        [Fact]
        public void ReturnsWhetherPossibleToCastSpellsAtCertainLevel()
        {
            Assert.False(domainCasting.HasSpells(0));
            Assert.True(domainCasting.HasSpells(1));
            Assert.False(domainCasting.HasSpells(2));
        }

        [Fact]
        public void DifficultyClassIsCalculatedBasedOnAbilityAndSpellLevel()
        {
            Assert.Equal(11, domainCasting.GetDifficultyClass(1));
            cleric.AbilityScores.SetScore(AbilityScoreTypes.Wisdom, 16);
            Assert.Equal(14, domainCasting.GetDifficultyClass(1));
        }
    }
}