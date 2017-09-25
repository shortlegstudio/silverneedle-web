// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.Attacks
{
    using Xunit;
    using System;
    using SilverNeedle;
    using SilverNeedle.Characters.Attacks;

    public class BaseAttackBonusTests
    {
        [Theory]
        [InlineData(1, 1)]
        [InlineData(5, 1)]
        [InlineData(6, 2)]
        [InlineData(10, 2)]
        [InlineData(11, 3)]
        [InlineData(15, 3)]
        [InlineData(16, 4)]
        [InlineData(20, 4)]
        public void IfBaseAttackBonusIsGreaterThanSixGrantAnExtraAttack(int baseAttackValue, int attacks)
        {
            var baseAttack = new BaseAttackBonus();
            baseAttack.SetValue(baseAttackValue);
            Assert.Equal(attacks, baseAttack.NumberOfAttacks);
        }

        [Theory]
        [InlineData(1, 1, 1)]
        [InlineData(5, 1, 5)]
        [InlineData(6, 2, 1)]
        [InlineData(6, 1, 6)]
        [InlineData(11, 1, 11)]
        [InlineData(11, 2, 6)]
        [InlineData(11, 3, 1)]
        [InlineData(16, 4, 1)]
        [InlineData(16, 2, 11)]
        [InlineData(16, 1, 16)]
        public void ReturnsAdjustedBaseAttackBonusForExtraAttacks(int baseAttackBonus, int attack, int expectedBonus)
        {
            var bab = new BaseAttackBonus();
            bab.SetValue(baseAttackBonus);
            Assert.Equal(expectedBonus, bab.GetAttackBonus(attack));
        }

        [Fact]
        public void TryingToGetAnAttackBonusForAnInvalidAttackThrowsException()
        {
            var bab = new BaseAttackBonus();
            Assert.Throws(typeof(ForbiddenByRulesException), () => bab.GetAttackBonus(30));
        }
    }

}