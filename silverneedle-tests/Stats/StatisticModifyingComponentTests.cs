// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Stats
{
    using System.Linq;
    using Xunit;
    using SilverNeedle;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Serialization;
    
    public class StatisticModifyingComponentTests
    {
        [Fact]
        public void ModifiesSkillsInCharacterSheet()
        {
            //TODO: This is so wordy to set up a test...
            var configuration = new MemoryStore();
            var modifiers = new MemoryStore();
            var nature = new MemoryStore();
            nature.SetValue("name", "Perception");
            nature.SetValue("modifier", 2);
            nature.SetValue("type", "bonus");
            modifiers.AddListItem(nature);
            configuration.SetValue("modifiers", modifiers);

            var modifySkill = new StatisticModifyingComponent(configuration);

            var bob = CharacterTestTemplates.WithSkills(new string[] { "Perception" });
            var oldPercept = bob.SkillRanks.GetScore("Perception");
            bob.Add(modifySkill);
            Assert.Equal(oldPercept + 2, bob.SkillRanks.GetScore("Perception"));
        }

        [Fact]
        public void ThrowsStatisticNotFoundExceptionIfItCannotFigureOutWhatToModify()
        {
            var configuration = new MemoryStore();
            var modifiers = new MemoryStore();
            var nature = new MemoryStore();
            nature.SetValue("name", "Made Up Statistic");
            nature.SetValue("modifier", 2);
            nature.SetValue("type", "bonus");
            modifiers.AddListItem(nature);
            configuration.SetValue("modifiers", modifiers);
            var modifySkill = new StatisticModifyingComponent(configuration);

            var bob = CharacterTestTemplates.AverageBob();
            Assert.Throws(typeof(StatisticNotFoundException), () => bob.Add(modifySkill));
        }

        [Fact]
        public void SupportsConditionalStatisticModifiers()
        {
            var configuration = new MemoryStore();
            var condModifiers = new MemoryStore();
            var saves = new MemoryStore();
            saves.SetValue("name", "Will");
            saves.SetValue("modifier", 4);
            saves.SetValue("type", "bonus");
            saves.SetValue("condition", "vs. Smoke");
            condModifiers.AddListItem(saves);
            configuration.SetValue("conditional-modifiers", condModifiers);
            var modifySaves = new StatisticModifyingComponent(configuration);
            var bob = CharacterTestTemplates.AverageBob();
            bob.Add(modifySaves);
            var willSave = bob.Defense.WillSave;
            Assert.Equal(willSave.TotalValue + 4, willSave.GetConditionalValue("vs. Smoke"));

        }
    }
}