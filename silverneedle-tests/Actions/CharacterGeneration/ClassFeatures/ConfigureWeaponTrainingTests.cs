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
    using SilverNeedle.Equipment;
    using SilverNeedle.Utility;
    using SilverNeedle.Serialization;


    
    public class ConfigureWeaponTrainingTests
    {
        [Fact]
        public void ChoosesAWeaponGroupForBonuses()
        {
            var store = new MemoryStore();
            store.SetValue("level", 1);
            var training = new ConfigureWeaponTraining(store);
            var character = new CharacterSheet(CharacterStrategy.Default());
            training.ExecuteStep(character, new CharacterStrategy());
            var ability = character.Components.Get<WeaponTraining>();
            Assert.NotNull(ability);
        }


        [Fact]
        public void IfLevelingUpCreateANewWeaponTrainingGroupAndIncreaseExistingLevel()
        {
            var store = new MemoryStore();
            store.SetValue("level", 2);
            var training = new ConfigureWeaponTraining(store);
            var character = new CharacterSheet(CharacterStrategy.Default());
            var wpnTrain1 = new WeaponTraining(WeaponGroup.Axes, 1);
            character.Add(wpnTrain1);
            training.ExecuteStep(character, new CharacterStrategy());
        
            var trainings = character.Components.GetAll<WeaponTraining>();
            var wpnTrain2 = trainings.First(x => x != wpnTrain1);
            Assert.Equal(trainings.Count(), 2);
            Assert.Equal(wpnTrain1.Level, 2);
            Assert.Equal(wpnTrain2.Level, 1);
            Assert.NotEqual(wpnTrain1.Group, wpnTrain2.Group);
        }
    }
}