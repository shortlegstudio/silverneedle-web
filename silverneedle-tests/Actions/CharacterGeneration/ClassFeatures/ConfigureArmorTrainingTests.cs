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
            var character = new CharacterSheet();
            character.InitializeComponents();
            step.ExecuteStep(character, new CharacterStrategy());
            var ability = character.SpecialQualities.SpecialAbilities.First();
            Assert.IsType<SilverNeedle.Characters.SpecialAbilities.ArmorTraining>(ability);
        }

        [Fact]
        public void IncreaseLevelOfArmorTrainingIfChanging()
        {
            var data = new MemoryStore();
            data.SetValue("level", 3);
            var step = new ConfigureArmorTraining(data);
            var at = new ArmorTraining();
            at.SetLevel(2);
            var character = new CharacterSheet();
            character.Add(at);
            step.ExecuteStep(character, new CharacterStrategy());
            Assert.Equal(at.Level, 3);
        }
    }
}