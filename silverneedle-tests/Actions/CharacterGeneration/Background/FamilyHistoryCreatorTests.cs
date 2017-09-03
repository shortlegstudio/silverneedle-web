// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions
{
    using Xunit;
    using SilverNeedle.Actions.CharacterGeneration.Background;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Background;

    
    public class FamilyHistoryCreatorTests : RequiresDataFiles
    {
        [Fact]
        public void CanCreateFamilyTreeWithParents()
        {
            var generator = new FamilyHistoryCreator();
            var character = CharacterTestTemplates.AverageBob();
            generator.ExecuteStep(character, new CharacterBuildStrategy());
            
            var familyTree = character.Get<History>().FamilyTree;

            Assert.NotNull(familyTree.Father);
            Assert.NotNull(familyTree.Mother);
            Assert.NotEmpty(familyTree.Father);
            Assert.NotEmpty(familyTree.Mother);
        }

        [Fact]
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
            gen.ExecuteStep(character, new CharacterBuildStrategy());
            var names = history.FamilyTree.Father + " " + history.FamilyTree.Mother;
            Assert.Contains("BarOrSomethingCrazyThatWontHappenAccidentally", names);
        }
    }
}

