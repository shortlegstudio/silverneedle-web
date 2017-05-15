// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions
{
    using NUnit.Framework;
    using SilverNeedle.Actions.CharacterGenerator.Background;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Background;

    [TestFixture]
    public class FamilyHistoryCreatorTests
    {
        [Test]
        public void CanCreateFamilyTreeWithParents()
        {
            var generator = new FamilyHistoryCreator();
            var familyTree = generator.CreateFamilyTree("Human", "FoodlePoodle");

            Assert.NotNull(familyTree.Father);
            Assert.NotNull(familyTree.Mother);
            Assert.IsNotEmpty(familyTree.Father);
            Assert.IsNotEmpty(familyTree.Mother);
        }

        [Test]
        public void AtLeastOneParentHasTheSameLastNameAsTheCharacter()
        {
            var race = new Race();
            race.Name = "Human";
            var character = new CharacterSheet();
            character.SetRace(race);
            character.FirstName = "Foo";
            character.LastName = "BarOrSomethingCrazyThatWontHappenAccidentally";
            var history = character.Get<History>();

            var gen = new FamilyHistoryCreator();
            gen.Process(character, new CharacterBuildStrategy());
            var names = history.FamilyTree.Father + " " + history.FamilyTree.Mother;
            Assert.That(names, Contains.Substring("BarOrSomethingCrazyThatWontHappenAccidentally"));
        }
    }
}

