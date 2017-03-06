// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGenerator
{
    using System.Linq;
    using NUnit.Framework;
    using SilverNeedle.Actions;
    using SilverNeedle.Actions.CharacterGenerator;
    using SilverNeedle.Utility;

    [TestFixture]
    public class CharacterCreatorTests
    {
        [Test]
        public void CharacterCreatorLoadsFromYamlStepsNecessary()
        {
            var data = new MemoryStore();
            data.SetValue("name", "Test One");
            var steps = new MemoryStore();
            steps.AddListItem(new MemoryStore("step", "Tests.Actions.CharacterGenerator.DummyStepOne"));
            steps.AddListItem(new MemoryStore("step", "Tests.Actions.CharacterGenerator.DummyStepTwo"));
            data.SetValue("level-one-steps", steps);

            var subject = new CharacterCreator(data);
            Assert.AreEqual("Test One", subject.Name);
            Assert.AreEqual(2, subject.FirstLevelSteps.Count());
        }
    }

    public class DummyStepOne : IBuildStep
    {
        
    }

    public class DummyStepTwo : IBuildStep
    {

    }
}