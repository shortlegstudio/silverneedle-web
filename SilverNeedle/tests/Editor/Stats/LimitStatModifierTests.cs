// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests
{
    using NUnit.Framework;
    using SilverNeedle;
    using SilverNeedle.Characters;

    [TestFixture]
    public class LimitStatModifierTests
    {
        [Test]
        public void CanTakeTwoStatsAndReturnValueFromTheLimitingValue()
        {
            var statOne = new BasicStat("Dex", 4);
            var statTwo = new BasicStat("Max", 1);

            var mod = new LimitStatModifier(statOne, statTwo);
            Assert.That(mod.Modifier, Is.EqualTo(1));
            Assert.That(mod.StatisticName, Is.EqualTo("Dex"));
            Assert.That(mod.Reason, Is.EqualTo("Max"));
            Assert.That(mod.Type, Is.EqualTo("Maximum"));
        }

        [Test]
        public void WorksOffOfTheAbilityModifierIfAbilityScoreIsUsed()
        {
            var ability = new AbilityScore(AbilityScoreTypes.Strength, 18);
            var limit = new BasicStat("Foo", 10);
            var mod = new LimitStatModifier(ability, limit);
            Assert.That(mod.Modifier, Is.EqualTo(4));
        }
    }
}