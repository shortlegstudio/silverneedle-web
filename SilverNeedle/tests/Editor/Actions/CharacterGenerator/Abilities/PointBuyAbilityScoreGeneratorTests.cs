// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGenerator.Abilities
{
    using NUnit.Framework;
    using SilverNeedle.Actions.CharacterGenerator.Abilities;
    using SilverNeedle.Characters;

    [TestFixture]
    public class PointBuyAbilityScoreGeneratorTests
    {
        [Test]
        public void StartsFromAverageAbilityScoreAndThenSpendsAllPointsPossible()
        {
            var gen = new PointBuyAbilityScoreGenerator();
            var scores = gen.GetScores();
            Assert.Ignore("Need to figure out how to calculate the ability scores");
        }
    }
}