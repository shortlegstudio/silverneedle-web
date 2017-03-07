// //-----------------------------------------------------------------------
// // <copyright file="FamilyTreeTests.cs" company="Short Leg Studio, LLC">
// //     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------
using System;
using NUnit.Framework;
using SilverNeedle.Actions.CharacterGenerator.Background;
using SilverNeedle.Actions.NamingThings;


namespace Actions
{
    [TestFixture]
    public class FamilyHistoryCreatorTests
    {
        [Test]
        public void CanCreateFamilyTreeWithParents()
        {
            var generator = new FamilyHistoryCreator();
            var familyTree = generator.CreateFamilyTree("Human");

            Assert.NotNull(familyTree.Father);
            Assert.NotNull(familyTree.Mother);
            Assert.IsNotEmpty(familyTree.Father);
            Assert.IsNotEmpty(familyTree.Mother);
        }
    }
}

