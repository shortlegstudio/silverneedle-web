// //-----------------------------------------------------------------------
// // <copyright file="CreateFamilyHistory.cs" company="Short Leg Studio, LLC">
// //     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------
using System;
using System.Linq;
using SilverNeedle.Actions.NamingThings;
using SilverNeedle.Characters;
using SilverNeedle.Characters.Background;
using SilverNeedle.Serialization;

namespace SilverNeedle.Actions.CharacterGeneration.Background
{
    public class FamilyHistoryCreator : ICharacterDesignStep
    {
        private NameCharacter namer = new NameCharacter();
        private EntityGateway<Occupation> occupations;

        public FamilyHistoryCreator()
        {
            this.occupations = GatewayProvider.Get<Occupation>();
        }

        public FamilyHistoryCreator(EntityGateway<Occupation> occupationGateway)
        {
            this.occupations = occupationGateway;
        }

        private void NameParents(FamilyTree familyTree, string race, string lastName)
        {
            var matcher = EnumHelpers.ChooseOne<SurnameMatcher>();

            familyTree.Father.FirstName = namer.GetFirstName(Gender.Male, race);
            familyTree.Mother.FirstName = namer.GetFirstName(Gender.Female, race);
            if(matcher == SurnameMatcher.MotherMatch)
            {
                familyTree.Father.LastName = namer.GetLastName(race);
            } else {
                familyTree.Father.LastName = lastName;
            }
            if(matcher == SurnameMatcher.FatherMatch)
            {
                familyTree.Mother.LastName = namer.GetLastName(race);
            } else {
                familyTree.Mother.LastName = lastName;
            }
        }

        private void ChooseParentOccupations(FamilyTree familyTree, BirthCircumstance birthCircumstance)
        {
            var availableOccupations = occupations.Where(x => x.MatchAnyTags(birthCircumstance.ParentProfessions)).ToList();

            if(availableOccupations.Empty())
                availableOccupations.Add(Occupation.Unemployed());

            var fatherOccupation = availableOccupations.ChooseOne();
            var motherOccupation = availableOccupations.ChooseOne();
            
            familyTree.Father.Add(fatherOccupation);
            familyTree.Mother.Add(motherOccupation);

        }

        public void ExecuteStep(CharacterSheet character)
        {
            var history = character.Get<History>();
            NameParents(history.FamilyTree, character.Race.Name, character.LastName);
            ChooseParentOccupations(history.FamilyTree, history.BirthCircumstance);
        }

        private enum SurnameMatcher
        {
            BothMatch,
            FatherMatch,
            MotherMatch
        }
    }
}

