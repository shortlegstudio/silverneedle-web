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

            var levelupSteps = new MemoryStore();
            levelupSteps.AddListItem(new MemoryStore("step", "Tests.Actions.CharacterGenerator.DummyStepThree"));
            data.SetValue("level-up-steps", levelupSteps);

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

        [Test]
        public void ProcessLevelUpExecutesSpecialSteps()
        {
            var characterSheet = new CharacterSheet();
            var strategy = new CharacterBuildStrategy();
            subject.ProcessLevelUp(characterSheet, strategy);
            
            //Does not run first level steps
            Assert.AreNotEqual("Dummy One", characterSheet.Name);

            //Runs level up
            Assert.AreEqual(20, characterSheet.MaxHitPoints);
        }
    }

    public class DummyStepOne : ICharacterBuildStep
    {
        public void ProcessFirstLevel(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            character.Name = "Dummy One";
        }

        public void ProcessLevelUp(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            throw new NotImplementedException();
        }
    }

    public class DummyStepTwo : ICharacterBuildStep
    {
        public void ProcessFirstLevel(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            character.AbilityScores.SetScore(AbilityScoreTypes.Strength, 16);
        }

        public void ProcessLevelUp(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            throw new NotImplementedException();
        }
    }

    public class DummyStepThree : ICharacterBuildStep
    {
        public void ProcessFirstLevel(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            throw new NotImplementedException();
        }

        public void ProcessLevelUp(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            character.IncreaseHitPoints(20);
        }
    }
}