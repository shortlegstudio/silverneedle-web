// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.Attacks
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Attacks;

    public class RangeAttackBonusTests
    {
        [Fact]
        public void UsesBaseAttackBonusAsAFoundation()
        {
            var bob = CharacterTestTemplates.AverageBob();
            var rangeAttack = bob.Get<RangeAttackBonus>();
            Assert.Equal(0, rangeAttack.TotalValue);
            bob.Offense.BaseAttackBonus.SetValue(10);
            Assert.Equal(10, rangeAttack.TotalValue);
        }

        [Fact]
        public void SizeModifiesAttackBonus()
        {
            var bob = CharacterTestTemplates.AverageBob();
            var rangeAttack = bob.Get<RangeAttackBonus>();
            Assert.Equal(0, rangeAttack.TotalValue);
            bob.Size.SetSize(CharacterSize.Small, 10, 10);
            Assert.Equal(1, rangeAttack.TotalValue);
        }

        [Fact]
        public void DexterityModifiesAttackBonus()
        {
            var bob = CharacterTestTemplates.AverageBob();
            var range = bob.Get<RangeAttackBonus>();
            Assert.Equal(0, range.TotalValue);
            bob.AbilityScores.SetScore(AbilityScoreTypes.Dexterity, 8);
            Assert.Equal(-1, range.TotalValue);
        }
    }
}