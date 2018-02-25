// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.Prerequisites
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Prerequisites;

    
    public class ClassLevelPrerequisiteTests
    {
        ClassLevelPrerequisite prereq;

        public ClassLevelPrerequisiteTests()
        {
            prereq = new ClassLevelPrerequisite("Fighter 4");
        }

        [Fact]
        public void CharacterIsQualifiedIfHasTheSameClassAtAppropriateLevel()
        {
            var character = new CharacterSheet(CharacterStrategy.Default());
            var fighter = Class.CreateForTesting();
            fighter.Name = "Fighter";
            character.SetClass(fighter);
            character.SetLevel(4);
            Assert.True(prereq.IsQualified(character.Components));
        }


        [Fact]
        public void CharacterIsNotQualifiedIfWrongClassButRightLevel()
        {
            var character = new CharacterSheet(CharacterStrategy.Default());
            var wizard = Class.CreateForTesting();
            wizard.Name = "Wizard";
            character.SetClass(wizard);
            character.SetLevel(4);
            Assert.False(prereq.IsQualified(character.Components));
        }

        [Fact]
        public void CharacterIsNotQualifiedIfRightClassButToLowLevel()
        {
            var character = new CharacterSheet(CharacterStrategy.Default());
            var fighter = Class.CreateForTesting();
            fighter.Name = "Fighter";
            character.SetClass(fighter);
            character.SetLevel(3);
            Assert.False(prereq.IsQualified(character.Components));
        }

        [Fact]
        public void IfCharacterIsNotSetToAClassYouDefinitelyDoNotQualify()
        {
            var character = new CharacterSheet(CharacterStrategy.Default());
            Assert.False(prereq.IsQualified(character.Components));
        }
    }
}