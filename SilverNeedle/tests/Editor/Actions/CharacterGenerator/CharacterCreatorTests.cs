// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGenerator
{
    using System;
    using System.Linq;
    using NUnit.Framework;
    using SilverNeedle.Actions;
    using SilverNeedle.Actions.CharacterGenerator;
    using SilverNeedle.Characters;
    using SilverNeedle.Utility;

    [TestFixture]
    public class CharacterCreatorTests
    {
        CharacterCreator subject;
        
        [SetUp]
        public void SetUpCharacterCreator()
        {
            var data = new MemoryStore();
            data.SetValue("name", "Test One");
            var steps = new MemoryStore();
            steps.AddListItem(new MemoryStore("step", "Tests.Actions.CharacterGenerator.DummyStepOne"));
            steps.AddListItem(new MemoryStore("step", "Tests.Actions.CharacterGenerator.DummyStepTwo"));
            data.SetValue("level-one-steps", steps);

            subject = new CharacterCreator(data);
        }

        [Test]
        public void CharacterCreatorLoadsFromYamlStepsNecessary()
        {           
            Assert.AreEqual("Test One", subject.Name);
            Assert.AreEqual(2, subject.FirstLevelSteps.Count());
        }

        [Test]
        public void CharacterCreatorExecutesEachBuildStepInSequence()
        {
            var characterSheet = new CharacterSheet();
            var strategy = new CharacterBuildStrategy();
            subject.ProcessFirstLevel(characterSheet, strategy);
            Assert.AreEqual("Dummy One", characterSheet.Name);
            Assert.AreEqual(16, characterSheet.AbilityScores.GetScore(AbilityScoreTypes.Strength));
        }
    }

    public class DummyStepOne : ICharacterBuildStep
    {
        public void ProcessFirstLevel(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            character.Name = "Dummy One";
        }
    }

    public class DummyStepTwo : ICharacterBuildStep
    {
        public void ProcessFirstLevel(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            character.AbilityScores.SetScore(AbilityScoreTypes.Strength, 16);
        }
    }
}