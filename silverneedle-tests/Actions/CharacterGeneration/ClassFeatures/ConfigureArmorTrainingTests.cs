// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGeneration.ClassFeatures
{
    using System.Linq;
    using Xunit;
    using SilverNeedle.Actions.CharacterGeneration.ClassFeatures;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Serialization;

    
    public class ConfigureArmorTrainingTests
    {
        [Fact]
        public void HookUpArmorTrainingToCharacterSheet()
        {
            var data = new MemoryStore();
            data.SetValue("level", 1);
            var step = new ConfigureArmorTraining(data);
            var character = new CharacterSheet(CharacterStrategy.Default());
            character.InitializeComponents();
            step.ExecuteStep(character);
            var ability = character.Get<ArmorTraining>();
            Assert.NotNull(ability);
        }

        [Fact]
        public void IncreaseLevelOfArmorTrainingIfChanging()
        {
            var data = new MemoryStore();
            data.SetValue("level", 3);
            var step = new ConfigureArmorTraining(data);
            var at = new ArmorTraining();
            at.SetLevel(2);
            var character = new CharacterSheet(CharacterStrategy.Default());
            character.Add(at);
            step.ExecuteStep(character);
            Assert.Equal(at.Level, 3);
        }
    }
}