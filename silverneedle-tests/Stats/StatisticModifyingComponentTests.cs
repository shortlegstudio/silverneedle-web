// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Stats
{
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
    }
}