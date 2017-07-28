// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.Background
{
    using Xunit;
    using SilverNeedle.Characters.Background;

    
    public class HistoryTests
    {
        [Fact]
        public void InitializeWithEmptyObjects()
        {
            var history = new History();
            Assert.NotNull(history.ClassOriginStory);
            Assert.NotNull(history.Drawback);
            Assert.NotNull(history.FamilyTree);
            Assert.NotNull(history.Homeland);
        }
    }
}