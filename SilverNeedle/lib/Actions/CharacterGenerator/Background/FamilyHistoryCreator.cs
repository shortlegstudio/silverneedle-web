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
    public class FamilyHistoryCreator
    {
        private INameCharacter namer;

        public FamilyHistoryCreator(INameCharacter namer)
        {
            this.namer = namer;
        }

        public FamilyTree CreateFamilyTree(string race)
        {
            var familyTree = new FamilyTree();
            familyTree.Father = namer.CreateFullName(Gender.Male, race);
            familyTree.Mother = namer.CreateFullName(Gender.Female, race);

            return familyTree;
        }
    }
}

