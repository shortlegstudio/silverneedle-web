// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Stats
{
    using NUnit.Framework;
    using SilverNeedle;
    using SilverNeedle.Characters;

    [TestFixture]
    public class AbilityStatModifierTests
    {
        [Test]
        public void AbilityStatModifiersTrackChangesToAbility()
        {
            var ability = new AbilityScore(AbilityScoreTypes.Strength, 10);

            var modifier = new AbilityStatModifier(ability);
            Assert.AreEqual(0, modifier.Modifier);
            ability.SetValue(20);
            Assert.AreEqual(05, modifier.Modifier);
        }
    }
}