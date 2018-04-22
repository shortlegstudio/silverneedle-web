// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Test.Core
{
    using Xunit;
    using SilverNeedle;
    using SilverNeedle.Characters;

    public class LimitStatModifierTests
    {
        [Fact]
        public void WorksOffOfTheAbilityModifierIfAbilityScoreIsUsed()
        {
            var ability = new AbilityScore(AbilityScoreTypes.Strength, 18);
            var limit = new BasicStat("Foo", 10);
            var mod = new LimitStatModifier("Foobar", ability, limit);
            Assert.Equal(mod.Modifier, 4);
        }
    }
}