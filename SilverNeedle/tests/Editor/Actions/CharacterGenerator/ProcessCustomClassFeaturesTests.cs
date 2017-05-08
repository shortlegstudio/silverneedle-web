// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGenerator
{
    using NUnit.Framework;
    using SilverNeedle.Actions;
    using SilverNeedle.Actions.CharacterGenerator;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;

    [TestFixture]
    public class ProcessCustomClassFeaturesTests
    {
        [Test]
        public void RunAnyCustomStepsForThisLevel()
        {
            var cls = new Class();
            var level = new Level(1);
            level.Steps.Add(new DummyCharacterDesignStep());
            cls.Levels.Add(level);
            var character = new CharacterSheet();
            character.SetClass(cls);

            var subject = new ProcessCustomClassFeatures();
            subject.Process(character, new CharacterBuildStrategy());
            Assert.That(character.Name, Is.EqualTo("I Ran!"));
        }

        [Test]
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
            subject.Process(character, new CharacterBuildStrategy());
            Assert.That(character.Components.GetAll<SpecialAbility>(), Contains.Item(ability));
        }

        public class DummyCharacterDesignStep : ICharacterDesignStep
        {
            public void Process(CharacterSheet character, CharacterBuildStrategy strategy)
            {
                character.Name = "I Ran!";
            }
        }
    }

}