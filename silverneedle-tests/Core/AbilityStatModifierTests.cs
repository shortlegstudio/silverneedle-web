// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Core
{
    using Xunit;
    using SilverNeedle;
    using SilverNeedle.Characters;

    public class AbilityStatModifierTests
    {
        [Fact]
        public void AbilityStatModifiersTrackChangesToAbility()
        {
            var ability = new AbilityScore(AbilityScoreTypes.Strength, 10);

            var modifier = new AbilityStatModifier(ability);
            Assert.Equal(0, modifier.Modifier);
            ability.SetValue(20);
            Assert.Equal(05, modifier.Modifier);
        }
    }
}