// Copyright (c) 2016 Trevor Redfern
// 
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php

namespace Characters
{
    using System.Linq;
    using NUnit.Framework;
    using SilverNeedle.Characters;
    using SilverNeedle.Utility;

    [TestFixture]
    public class CharacterBuildStrategyTests
    {
        [Test]
        public void AnEmptyStrategySelectsAllAttributesEvenly()
        {
            var strategy = new CharacterBuildStrategy();
            Assert.AreEqual(6, strategy.FavoredAbilities.All().Count());
        }

        [Test]
        public void CanLoadFromObjectStore()
        {
            Assert.Ignore("Not built yet");
        }
    }
}