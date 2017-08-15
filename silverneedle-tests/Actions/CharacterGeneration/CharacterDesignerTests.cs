// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGeneration
{
    using System.Linq;
    using Xunit;
    using SilverNeedle.Actions;
    using SilverNeedle.Actions.CharacterGeneration;
    using SilverNeedle.Characters;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    
    public class CharacterDesignerTests
    {
        CharacterDesigner subject;
        
        public CharacterDesignerTests()
        {
            var data = new MemoryStore();
            data.SetValue("name", "Test One");
            var steps = new MemoryStore();
            steps.AddListItem(new MemoryStore("step", "Tests.Actions.CharacterGeneration.DummyStepOne"));
            steps.AddListItem(new MemoryStore("step", "Tests.Actions.CharacterGeneration.DummyStepTwo"));
            data.SetValue("steps", steps);

            subject = new CharacterDesigner(data);
        }

        [Fact]
        public void DefaultTypeForDesignerIsNormal()
        {
            Assert.Equal(CharacterDesigner.Type.Normal, subject.DesignerType);
        }

        [Fact]
        public void CharacterCreatorLoadsFromYamlStepsNecessary()
        {           
            Assert.Equal("Test One", subject.Name);
            Assert.Equal(2, subject.Steps.Count());
        }

        [Fact]
        public void CharacterCreatorExecutesEachBuildStepInSequence()
        {
            var characterSheet = new CharacterSheet();
            var strategy = new CharacterBuildStrategy();
            subject.Process(characterSheet, strategy);
            Assert.Equal("Dummy One", characterSheet.Name);
            Assert.Equal(16, characterSheet.AbilityScores.GetScore(AbilityScoreTypes.Strength));
        }

        [Fact]
        public void CharacterDesignerWillExecuteStepsUntilCharacterIsAtStrategyLevelIfTypeIsLevelUp()
        {
            var data = new MemoryStore();
            data.SetValue("name", "Test One");
            data.SetValue("type", "levelup");
            var steps = new MemoryStore();
            steps.AddListItem(new MemoryStore("step", "Tests.Actions.CharacterGeneration.DummyStepLevelUp"));
            data.SetValue("steps", steps);
            
            var designer = new CharacterDesigner(data);
            var character = new CharacterSheet();
            character.SetClass(new Class());
            var build = new CharacterBuildStrategy();
            build.TargetLevel = 5;

            designer.Process(character, build);
            Assert.Equal(designer.DesignerType, CharacterDesigner.Type.LevelUp);
            Assert.Equal(character.Level, 5);
            Assert.Equal(character.Age, 4);
        }

        public void IfLevelUpDoesNotIncrementLevelThrowException()
        {
            var data = new MemoryStore();
            data.SetValue("name", "Test One");
            data.SetValue("type", "levelup");

            //Does now steps that increment level
            var steps = new MemoryStore();
            data.SetValue("steps", steps);

            var character = new CharacterSheet();
            var build = new CharacterBuildStrategy();
            build.TargetLevel = 5;

            var designer = new CharacterDesigner(data);
            Assert.Throws<System.InvalidOperationException>(() => designer.Process(character, build));
        }
    }

    public class DummyStepOne : ICharacterDesignStep
    {
        public void Process(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            character.FirstName = "Dummy";
            character.LastName = "One";
        }
    }

    public class DummyStepTwo : ICharacterDesignStep
    {
        public void Process(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            character.AbilityScores.SetScore(AbilityScoreTypes.Strength, 16);
        }
    }

    public class DummyStepLevelUp : ICharacterDesignStep
    {
        public void Process(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            character.Age += 1;
            character.SetLevel(character.Level + 1);
        }
    }
}