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
    using SilverNeedle.Serialization;

    [TestFixture]
    public class ConfigureArmorTrainingTests
    {
        [Test]
        public void HookUpArmorTrainingToCharacterSheet()
        {
            var data = new MemoryStore();
            data.SetValue("level", 1);
            var step = new ConfigureArmorTraining(data);
            var character = new CharacterSheet();
            step.Process(character, new CharacterBuildStrategy());
            var ability = character.SpecialQualities.SpecialAbilities.First();
            Assert.That(ability, Is.TypeOf<SilverNeedle.Characters.SpecialAbilities.ArmorTraining>());
        }

        [Test]
        public void IncreaseLevelOfArmorTrainingIfChanging()
        {
            var data = new MemoryStore();
            data.SetValue("level", 3);
            var step = new ConfigureArmorTraining(data);
            var at = new ArmorTraining(2);
            var character = new CharacterSheet();
            character.AddAbility(at);
            step.Process(character, new CharacterBuildStrategy());
            Assert.That(at.Level, Is.EqualTo(3));
        }
    }
}