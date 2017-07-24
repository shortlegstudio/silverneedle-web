// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.Prerequisites
{
    using NUnit.Framework;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Prerequisites;

    [TestFixture]
    public class ClassLevelPrerequisiteTests
    {
        ClassLevelPrerequisite prereq;

        [SetUp]
        public void Configure()
        {
            prereq = new ClassLevelPrerequisite("Fighter 4");
        }

        [Test]
        public void CharacterIsQualifiedIfHasTheSameClassAtAppropriateLevel()
        {
            var character = new CharacterSheet();
            var fighter = new Class();
            fighter.Name = "Fighter";
            character.SetClass(fighter);
            character.SetLevel(4);
            Assert.That(prereq.IsQualified(character), Is.True);
        }


        [Test]
        public void CharacterIsNotQualifiedIfWrongClassButRightLevel()
        {
            var character = new CharacterSheet();
            var wizard = new Class();
            wizard.Name = "Wizard";
            character.SetClass(wizard);
            character.SetLevel(4);
            Assert.That(prereq.IsQualified(character), Is.False);
        }

        [Test]
        public void CharacterIsNotQualifiedIfRightClassButToLowLevel()
        {
            var character = new CharacterSheet();
            var fighter = new Class();
            fighter.Name = "Fighter";
            character.SetClass(fighter);
            character.SetLevel(3);
            Assert.That(prereq.IsQualified(character), Is.False);
        }

        [Test]
        public void IfCharacterIsNotSetToAClassYouDefinitelyDoNotQualify()
        {
            var character = new CharacterSheet();
            Assert.That(prereq.IsQualified(character), Is.False);
        }
    }
}