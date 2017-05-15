// //-----------------------------------------------------------------------
// // <copyright file="CreateFamilyHistory.cs" company="Short Leg Studio, LLC">
// //     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------
using System;
using SilverNeedle.Actions.NamingThings;
using SilverNeedle.Characters;
using SilverNeedle.Characters.Background;

namespace SilverNeedle.Actions.CharacterGenerator.Background
{
    public class FamilyHistoryCreator : ICharacterDesignStep
    {
        private NameCharacter namer;

        public FamilyHistoryCreator()
        {
            this.namer = new NameCharacter();
        }

        public FamilyTree CreateFamilyTree(string race, string lastName)
        {
            var familyTree = new FamilyTree();
            var matcher = EnumHelpers.ChooseOne<SurnameMatcher>();

            if(matcher == SurnameMatcher.BothMatch || matcher == SurnameMatcher.FatherMatch)
            {
                familyTree.Father = namer.CreateFullName(Gender.Male, race, lastName);
            }
            else
            {
                familyTree.Father = namer.CreateFullName(Gender.Male, race);
            }
            if(matcher == SurnameMatcher.BothMatch || matcher == SurnameMatcher.MotherMatch)
            {
                familyTree.Mother = namer.CreateFullName(Gender.Female, race, lastName);
            }
            else
            {
                familyTree.Mother = namer.CreateFullName(Gender.Female, race);
            }

            return familyTree;
        }

        public void Process(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            character.History.FamilyTree = CreateFamilyTree(character.Race.Name, character.LastName);
        }

        private enum SurnameMatcher
        {
            BothMatch,
            FatherMatch,
            MotherMatch
        }
    }
}

