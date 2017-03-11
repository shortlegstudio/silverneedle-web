// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGenerator
{
    using System.Linq;
    using Moq;
    using NUnit.Framework;
    using SilverNeedle.Actions;
    using SilverNeedle.Actions.CharacterGenerator;
    using SilverNeedle.Characters;
    using SilverNeedle.Utility;

    [TestFixture]
    public class CharacterDesignerTests
    {
        CharacterDesigner subject;
        
        [SetUp]
        public void SetUpCharacterCreator()
        {
            var data = new MemoryStore();
            data.SetValue("name", "Test One");
            var steps = new MemoryStore();
            steps.AddListItem(new MemoryStore("step", "Tests.Actions.CharacterGenerator.DummyStepOne"));
            steps.AddListItem(new MemoryStore("step", "Tests.Actions.CharacterGenerator.DummyStepTwo"));
            data.SetValue("steps", steps);

            subject = new CharacterDesigner(data);
        }

        [Test]
        public void CharacterCreatorLoadsFromYamlStepsNecessary()
        {           
            Assert.AreEqual("Test One", subject.Name);
            Assert.AreEqual(2, subject.Steps.Count());
        }

        [Test]
        public void CharacterCreatorExecutesEachBuildStepInSequence()
        {
            var characterSheet = new CharacterSheet();
            var strategy = new CharacterBuildStrategy();
            subject.Process(characterSheet, strategy);
            Assert.AreEqual("Dummy One", characterSheet.Name);
            Assert.AreEqual(16, characterSheet.AbilityScores.GetScore(AbilityScoreTypes.Strength));
        }

        [Test]
        public void DesignerStepsFindSpecificImplementationOfCharacterDesigners()
        { 
            // TODO: This test is dependent on yaml configuration
            var data = new MemoryStore();
            data.SetValue("name", "Test One");
            var steps = new MemoryStore();
            steps.AddListItem(new MemoryStore("designer", "special"));
            data.SetValue("steps", steps);

            
            subject = new CharacterDesigner(data);
            Assert.That(subject.Steps, Has.Exactly(1).TypeOf(typeof(DesignerExecuterStep)));
        }
    }

    public class DummyStepOne : ICharacterDesignStep
    {
        public void Process(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            character.Name = "Dummy One";
        }
    }

    public class DummyStepTwo : ICharacterDesignStep
    {
        public void Process(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            character.AbilityScores.SetScore(AbilityScoreTypes.Strength, 16);
        }
    }
}