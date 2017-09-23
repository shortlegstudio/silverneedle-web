// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.Attacks
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Attacks;
    using SilverNeedle.Equipment;

    public class MeleeAttackBonusTests
    {
        [Fact]
        public void MeleeAttackUsesTheBaseAttackBonusForTheCharacterAsABase()
        {
            var bob = CharacterTestTemplates.AverageBob();
            var calculator = bob.Get<MeleeAttackBonus>();
            bob.Offense.BaseAttackBonus.SetValue(10);
            Assert.Equal(10, calculator.TotalValue);
        }
    }
}