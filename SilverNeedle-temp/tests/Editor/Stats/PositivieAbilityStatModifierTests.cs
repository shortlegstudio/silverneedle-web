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
    public class PositiveAbilityStatModifierTests
    {
        [Test]
        public void IfAbilityIsNegativeModifierJustUseZero()
        {
            var ability = new AbilityScore(AbilityScoreTypes.Strength, 6);

            var modifier = new PositiveAbilityStatModifier(ability);
            Assert.That(modifier.Modifier, Is.EqualTo(0));

            //Setting the ability positive generates a good value
            ability.SetValue(20);
            Assert.That(modifier.Modifier, Is.EqualTo(5));
        }
    }
}