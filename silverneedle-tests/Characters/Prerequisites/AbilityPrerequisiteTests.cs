// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.Prerequisites
{
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Prerequisites;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    
    public class AbilityPrerequisiteTests {
        [Fact]
        public void AbilityIsQualifiedIfExceedingScore() {
            var pre = new AbilityPrerequisite (AbilityScoreTypes.Intelligence, 13);
            var c = new CharacterSheet ();
            c.AbilityScores.SetScore (AbilityScoreTypes.Intelligence, 15);
            Assert.True (pre.IsQualified (c));
        }

        [Fact]
        public void AbilityIsNotQualifiedIfNotExceedingScore() {
            var pre = new AbilityPrerequisite (AbilityScoreTypes.Intelligence, 13);
            var c = new CharacterSheet ();
            c.AbilityScores.SetScore (AbilityScoreTypes.Intelligence, 11);
            Assert.False (pre.IsQualified (c));
        }
    }
}