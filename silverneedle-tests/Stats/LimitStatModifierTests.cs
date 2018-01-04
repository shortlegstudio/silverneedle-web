// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests
{
    using Xunit;
    using SilverNeedle;
    using SilverNeedle.Characters;

    public class LimitStatModifierTests
    {
        [Fact]
        public void CanTakeTwoStatsAndReturnValueFromTheLimitingValue()
        {
            var statOne = new BasicStat("Dex", 4);
            var statTwo = new BasicStat("Max", 1);

            var mod = new LimitStatModifier(statOne, statTwo);
            Assert.Equal(mod.Modifier, 1);
            Assert.Equal(mod.StatisticName, "Dex");
            Assert.Equal(mod.Reason, "Max");
            Assert.Equal(mod.ModifierType, "Maximum");
        }

        [Fact]
        public void WorksOffOfTheAbilityModifierIfAbilityScoreIsUsed()
        {
            var ability = new AbilityScore(AbilityScoreTypes.Strength, 18);
            var limit = new BasicStat("Foo", 10);
            var mod = new LimitStatModifier(ability, limit);
            Assert.Equal(mod.Modifier, 4);
        }
    }
}