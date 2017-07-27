// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions {
    using NUnit.Framework;
    using SilverNeedle.Actions.CharacterGenerator.Abilities;
    [TestFixture]
    public class AverageAbilityScoreGeneratorTests 
    {
        [Fact]
        public void CreateAverageScores() 
        {
            var roller = new AverageAbilityScoreGenerator ();
            var scores = roller.GetScores();
            foreach(var v in scores)
            {
                Assert.That(v, Is.EqualTo(10));
            }
        }
    }
}