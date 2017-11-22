// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities.BloodlinePowers
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Attacks;
    using SilverNeedle.Characters.SpecialAbilities.BloodlinePowers;

    public class HeavenlyFireTests
    {
        [Fact]
        public void HeavenlyFireIsAnAttackThatChangesWithLevels()
        {
            var sorcerer = CharacterTestTemplates.Sorcerer();
            var heavenly = new HeavenlyFire();
            sorcerer.Add(heavenly);
            Assert.Equal(30, heavenly.Range);
            Assert.Equal(AttackTypes.Special, heavenly.AttackType);
            Assert.Equal(20, heavenly.CriticalThreat);
            Assert.Equal(2, heavenly.CriticalModifier.TotalValue);
            sorcerer.Offense.BaseAttackBonus.SetValue(3);
            Assert.Equal(3, heavenly.AttackBonus.TotalValue);

            Assert.Equal("1d4", heavenly.Damage.ToString());
            sorcerer.SetLevel(10);
            Assert.Equal("1d4+5", heavenly.Damage.ToString());

            Assert.Equal(3, heavenly.UsesPerDay);
            sorcerer.AbilityScores.SetScore(AbilityScoreTypes.Charisma, 16);
            Assert.Equal(6, heavenly.UsesPerDay);

        }
    }
}