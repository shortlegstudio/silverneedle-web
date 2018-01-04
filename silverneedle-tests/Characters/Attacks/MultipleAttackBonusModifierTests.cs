// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.Attacks
{
    using Xunit;
    using System.Linq;
    using SilverNeedle.Characters.Attacks;
    using SilverNeedle.Characters;

    public class MultipleAttackBonusModifierTests
    {
        [Theory]
        [InlineData(1, 0)]
        [InlineData(2, -5)]
        [InlineData(3, -10)]
        [InlineData(4, -15)]
        [InlineData(5, -20)]
        [InlineData(6, -25)]
        public void EachAttackPastOneIncreasesTheModifierByFive(int attack, int expectedModifier)
        {
            var mod = new MultipleAttackBonusModifier(attack);
            Assert.Equal(expectedModifier, mod.Modifier);
            Assert.Equal("extra attacks", mod.Reason);
            Assert.Equal("penalty", mod.ModifierType);
            Assert.Equal(StatNames.BaseAttackBonus, mod.StatisticName);
        }

        [Theory]
        [InlineData("attack 1", 0)]
        [InlineData("attack 2", -5)]
        [InlineData("attack 5", -20)]
        public void ProvideAStandardListOfModifiersToBeUsedRepeatedlyForThisCase(string condition, int expectedMod)
        {
            var mods = MultipleAttackBonusModifier.GetConditionalMultipleAttackModifiers();
            var attack = mods.First(x => x.Condition == condition);
            Assert.Equal(expectedMod, attack.Modifier); 
        }
    }
}