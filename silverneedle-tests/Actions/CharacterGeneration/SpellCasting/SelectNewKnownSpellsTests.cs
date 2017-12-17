// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGeneration.SpellCasting
{
    using System.Linq;
    using Xunit;
    using SilverNeedle.Actions.CharacterGeneration.SpellCasting;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Magic;

    public class SelectNewKnownSpellsTests
    {
        [Fact]
        public void SelectsSpellsForEachLevelUntilAllAvailableSlotsAreFilled()
        {
            var bard = CharacterTestTemplates.BardyBard().WithSpontaneousCasting();
            var selector = new SelectNewKnownSpells();
            selector.ExecuteStep(bard);
            var spellCasting = bard.Get<SpontaneousCasting>();
            Assert.Equal(spellCasting.GetKnownSpellCount(0), spellCasting.GetReadySpells(0).Count());
            Assert.Equal(spellCasting.GetKnownSpellCount(1), spellCasting.GetReadySpells(1).Count());
            Assert.Equal(spellCasting.GetKnownSpellCount(2), spellCasting.GetReadySpells(2).Count());

            bard.SetLevel(3);
            selector.ExecuteStep(bard);
            Assert.Equal(spellCasting.GetKnownSpellCount(0), spellCasting.GetReadySpells(0).Count());
            Assert.Equal(spellCasting.GetKnownSpellCount(1), spellCasting.GetReadySpells(1).Count());
            Assert.Equal(spellCasting.GetKnownSpellCount(2), spellCasting.GetReadySpells(2).Count());
        }

        [Theory]
        [Repeat(100)]
        public void DoNotSelectTheSameSpellTwice()
        {
            var bard = CharacterTestTemplates.BardyBard().WithSpontaneousCasting(6);
            var selector = new SelectNewKnownSpells();
            selector.ExecuteStep(bard);
            bard.SetLevel(2);
            selector.ExecuteStep(bard);
            bard.SetLevel(3);
            selector.ExecuteStep(bard);
            var spellCasting = bard.Get<SpontaneousCasting>();
            Assert.NotEmpty(spellCasting.GetReadySpells(0));
            Assert.NotEmpty(spellCasting.GetReadySpells(1));
            AssertExtensions.ListIsUnique(spellCasting.GetReadySpells(0));
            AssertExtensions.ListIsUnique(spellCasting.GetReadySpells(1));
        }
    }
}