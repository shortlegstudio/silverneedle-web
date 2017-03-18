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

    [TestFixture]
    public class SetupCustomClassFeaturesTests
    {

        [Test]
        public void ExecutesACustomBuildStepIfAssignedToCharacterClass()
        {
            var character = new CharacterSheet();
            var cls = new Class();
            cls.CustomBuildStep = "Tests.Actions.CharacterGenerator.CustomClassStep";
            character.SetClass(cls);

            var subject = new SetupCustomClassFeatures();
            subject.Process(character, new CharacterBuildStrategy());

            Assert.That(character.Offense.WeaponProficiencies[0].Name, Is.EqualTo("Turtles"));
        }

        [Test]
        public void ClassesWithoutCustomStepsDoNothing()
        {

            var character = new CharacterSheet();
            var cls = new Class();
            character.SetClass(cls);
            var subject = new SetupCustomClassFeatures();
            subject.Process(character, new CharacterBuildStrategy());
            Assert.That(character.Offense.WeaponProficiencies, Is.Empty);
        }
    }

    public class CustomClassStep : ICharacterDesignStep
    {
        public void Process(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            character.Offense.AddWeaponProficiency("turtles");
        }

    }
}