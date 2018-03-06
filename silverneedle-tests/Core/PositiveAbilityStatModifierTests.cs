// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace Tests.Core
{
    using Xunit;
    using SilverNeedle;
    using SilverNeedle.Characters;

    public class PositiveAbilityStatModifierTests
    {
        [Fact]
        public void IfAbilityIsNegativeModifierJustUseZero()
        {
            var ability = new AbilityScore(AbilityScoreTypes.Strength, 6);

            var modifier = new PositiveAbilityStatModifier(ability);
            Assert.Equal(modifier.Modifier, 0);

            //Setting the ability positive generates a good value
            ability.SetValue(20);
            Assert.Equal(modifier.Modifier, 5);
        }
    }
}