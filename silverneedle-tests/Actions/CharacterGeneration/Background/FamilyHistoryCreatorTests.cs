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
    using SilverNeedle.Serialization;

    
    public class FamilyHistoryCreatorTests : RequiresDataFiles
    {
        [Fact]
        public void CanCreateFamilyTreeWithParents()
        {
            var generator = new FamilyHistoryCreator();
            var character = CharacterTestTemplates.AverageBob().OfRace("Human");
            generator.ExecuteStep(character);
            
            var familyTree = character.Get<History>().FamilyTree;

            Assert.NotNull(familyTree.FatherName);
            Assert.NotNull(familyTree.MotherName);
            Assert.NotEmpty(familyTree.FatherName);
            Assert.NotEmpty(familyTree.MotherName);
        }

        [Fact]
        public void AtLeastOneParentHasTheSameLastNameAsTheCharacter()
        {
            var race = new Race();
            race.Name = "Human";
            var character = CharacterTestTemplates.AverageBob();
            character.Add(race);
            character.FirstName = "Foo";
            character.LastName = "BarOrSomethingCrazyThatWontHappenAccidentally";
            var history = character.Get<History>();

            var gen = new FamilyHistoryCreator();
            gen.ExecuteStep(character);
            var names = history.FamilyTree.FatherName + " " + history.FamilyTree.MotherName;
            Assert.Contains("BarOrSomethingCrazyThatWontHappenAccidentally", names);
        }

        [Fact]
        public void MotherAndFatherAreGivenJobsDependingOnTagsFromBirthCircumstance()
        {
            //Bob's parents were lower-class
            var bob = CharacterTestTemplates.AverageBob().OfRace("Human");
            bob.History.BirthCircumstance.ParentProfessions = new string[] { "lower-class" };

            var peasant = new Occupation("peasant", "commoner", new string[] { "lower-class" });
            var occGateway = EntityGateway<Occupation>.LoadWithSingleItem(peasant);
            
            var historyCreator = new FamilyHistoryCreator(occGateway);
            historyCreator.ExecuteStep(bob);

            Assert.Equal(peasant, bob.Get<History>().FamilyTree.Father.Get<Occupation>());
            Assert.Equal(peasant, bob.Get<History>().FamilyTree.Mother.Get<Occupation>());
        }

        [Fact]
        public void IfNoOccupationsMatchJustPickUnemployed()
        {
            var bob = CharacterTestTemplates.AverageBob().OfRace("Human");
            var historyCreator = new FamilyHistoryCreator();
            historyCreator.ExecuteStep(bob);
            
            Assert.Equal(Occupation.Unemployed(), bob.Get<History>().FamilyTree.Father.Get<Occupation>());
            Assert.Equal(Occupation.Unemployed(), bob.Get<History>().FamilyTree.Mother.Get<Occupation>());
        }

        [Fact]
        public void AddSomeWeightingToSkillsTableForParentOccupation()
        {
            var bob = CharacterTestTemplates.AverageBob().OfRace("Human");
            bob.History.BirthCircumstance.ParentProfessions = new string[] { "lower-class" };

            var peasant = new Occupation("peasant", "commoner", new string[] { "lower-class" });
            peasant.Skills = new string[] { "Perception" };
            var occGateway = EntityGateway<Occupation>.LoadWithSingleItem(peasant);
            
            var historyCreator = new FamilyHistoryCreator(occGateway);
            historyCreator.ExecuteStep(bob);

            Assert.True(bob.Strategy.FavoredSkills.HasOption("Perception"));

        }
    }
}

