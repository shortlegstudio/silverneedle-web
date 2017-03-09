// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions
{
    using NUnit.Framework;
    using SilverNeedle.Actions.CharacterGenerator.Background;

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

