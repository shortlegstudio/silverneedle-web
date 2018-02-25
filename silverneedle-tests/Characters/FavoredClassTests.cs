// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Dice;
    
    public class FavoredClassTests
    {
        [Fact]
        public void TracksAClassNameThatItKnowsWhetherTheClassMatches()
        {
            var favored = new FavoredClass("fighter");
            var fighter = Class.CreateForTesting("Fighter", DiceSides.d10); 
            Assert.True(favored.Qualifies(fighter));
        }

    }
}