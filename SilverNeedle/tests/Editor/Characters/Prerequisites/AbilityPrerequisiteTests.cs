// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.Prerequisites
{
    using System.Collections.Generic;
    using System.Linq;
    using NUnit.Framework;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Prerequisites;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    [TestFixture]
    public class AbilityPrerequisiteTests {
        [Test]
        public void AbilityIsQualifiedIfExceedingScore() {
            var pre = new AbilityPrerequisite (AbilityScoreTypes.Intelligence, 13);
            var c = new CharacterSheet (new List<Skill>());
            c.AbilityScores.SetScore (AbilityScoreTypes.Intelligence, 15);
            Assert.IsTrue (pre.IsQualified (c));
        }

        [Test]
        public void AbilityIsNotQualifiedIfNotExceedingScore() {
            var pre = new AbilityPrerequisite (AbilityScoreTypes.Intelligence, 13);
            var c = new CharacterSheet (new List<Skill>());
            c.AbilityScores.SetScore (AbilityScoreTypes.Intelligence, 11);
            Assert.IsFalse (pre.IsQualified (c));
        }
    }
}