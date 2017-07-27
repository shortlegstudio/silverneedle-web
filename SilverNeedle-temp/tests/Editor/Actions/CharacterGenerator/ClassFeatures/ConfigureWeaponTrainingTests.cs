// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGenerator.ClassFeatures
{
    using System.Linq;
    using NUnit.Framework;
    using SilverNeedle.Actions.CharacterGenerator.ClassFeatures;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Equipment;
    using SilverNeedle.Utility;
    using SilverNeedle.Serialization;


    [TestFixture]
    public class ConfigureWeaponTrainingTests
    {
        [Fact]
        public void ChoosesAWeaponGroupForBonuses()
        {
            var store = new MemoryStore();
            store.SetValue("level", 1);
            var training = new ConfigureWeaponTraining(store);
            var character = new CharacterSheet();
            training.Process(character, new CharacterBuildStrategy());
            var ability = character.Components.Get<WeaponTraining>();
            Assert.That(ability, Is.Not.Null);
        }


        [Fact]
        public void IfLevelingUpCreateANewWeaponTrainingGroupAndIncreaseExistingLevel()
        {
            var store = new MemoryStore();
            store.SetValue("level", 2);
            var training = new ConfigureWeaponTraining(store);
            var character = new CharacterSheet();
            var wpnTrain1 = new WeaponTraining(WeaponGroup.Axes, 1);
            character.Add(wpnTrain1);
            training.Process(character, new CharacterBuildStrategy());
        
            var trainings = character.Components.GetAll<WeaponTraining>();
            var wpnTrain2 = trainings.First(x => x != wpnTrain1);
            Assert.That(trainings.Count(), Is.EqualTo(2));
            Assert.That(wpnTrain1.Level, Is.EqualTo(2));
            Assert.That(wpnTrain2.Level, Is.EqualTo(1));
            Assert.That(wpnTrain1.Group, Is.Not.EqualTo(wpnTrain2.Group));
        }
    }
}