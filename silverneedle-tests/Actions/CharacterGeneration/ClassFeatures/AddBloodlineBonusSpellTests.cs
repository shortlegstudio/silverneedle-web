// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGeneration.ClassFeatures
{
    using System.Collections.Generic;
    using SilverNeedle.Actions.CharacterGeneration.ClassFeatures;
    using SilverNeedle.Characters.Magic;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Spells;
    using SilverNeedle.Serialization;
    using Xunit;

    public class AddBloodlineBonusSpellTests : RequiresDataFiles
    {
        [Fact]
        public void BonusSpellsAreAddedToTheAppropriateSpellLevel()
        {
            var sorcerer = CharacterTestTemplates.Sorcerer().WithSpontaneousCasting();
            var bloodline = Bloodline.CreateWithValues(
                "aberrant", 
                new string[] { "perception" },
                new Dictionary<int, string>(),
                new Dictionary<int, string> { { 1, "some random spell" } },
                new string[] { });
            
            sorcerer.Add(bloodline);
            var casting = sorcerer.Get<SpontaneousCasting>();
            var addSpells = new AddBloodlineBonusSpell(new MemoryStore("spell-level", "1"));
            addSpells.ExecuteStep(sorcerer);
            AssertExtensions.Contains("some random spell", casting.GetReadySpells(1));
        }

    }
}