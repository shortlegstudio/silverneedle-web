// Copyright (c) 2016 Trevor Redfern
// 
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php

namespace Characters
{
    using System.Linq;
    using NUnit.Framework;
    using SilverNeedle.Characters;
    using SilverNeedle.Yaml;

    [TestFixture]
    public class CharacterBuildStrategyTests
    {
        [Test]
        public void AnEmptyStrategyJustRepresentsDoEverythingRandomly()
        {
            var strategy = new CharacterBuildStrategy();
            
        }
    }
}