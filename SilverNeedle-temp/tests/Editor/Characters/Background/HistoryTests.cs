// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.Background
{
    using NUnit.Framework;
    using SilverNeedle.Characters.Background;

    [TestFixture]
    public class HistoryTests
    {
        [Fact]
        public void InitializeWithEmptyObjects()
        {
            var history = new History();
            Assert.That(history.ClassOriginStory, Is.Not.Null);
            Assert.That(history.Drawback, Is.Not.Null);
            Assert.That(history.FamilyTree, Is.Not.Null);
            Assert.That(history.Homeland, Is.Not.Null);
        }
    }
}