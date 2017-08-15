// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGeneration
{
    using Xunit;
    using SilverNeedle.Actions;
    using SilverNeedle.Actions.CharacterGeneration;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;

    
    public class ProcessCustomClassFeaturesTests
    {
        [Fact]
        public void RunAnyCustomStepsForThisLevel()
        {
            var cls = new Class();
            var level = new Level(1);
            level.Steps.Add(new DummyCharacterDesignStep());
            cls.Levels.Add(level);
            var character = new CharacterSheet();
            character.SetClass(cls);

            var subject = new ProcessCustomClassFeatures();
            subject.ExecuteStep(character, new CharacterBuildStrategy());
            Assert.Equal(character.Name, "I Ran!");
        }

        [Fact]
        public void AssignAbilitiesForThisLevel()
        {
            var cls = new Class();
            var level = new Level(1);
            var ability = new SpecialAbility();
            level.Abilities.Add(ability);
            cls.Levels.Add(level);
            var character = new CharacterSheet();
            character.SetClass(cls);

            var subject = new ProcessCustomClassFeatures();
            subject.ExecuteStep(character, new CharacterBuildStrategy());
            Assert.Contains(ability, character.Components.GetAll<SpecialAbility>());
        }

        public class DummyCharacterDesignStep : ICharacterDesignStep
        {
            public void ExecuteStep(CharacterSheet character, CharacterBuildStrategy strategy)
            {
                character.FirstName = "I Ran!";
            }
        }
    }

}