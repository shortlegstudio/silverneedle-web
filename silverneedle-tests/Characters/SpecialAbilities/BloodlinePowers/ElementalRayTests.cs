// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities.BloodlinePowers
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Characters.SpecialAbilities.BloodlinePowers;

    public class ElementalRayTests
    {
        private ElementalRay ray;
        private ElementalType type;
        private CharacterSheet sorcerer;
        public ElementalRayTests()
        {
            sorcerer = CharacterTestTemplates.Sorcerer();
            type = new ElementalType();
            type.EnergyType = "acid";
            sorcerer.Add(type);
            ray = new ElementalRay();
            sorcerer.Add(ray);
        }
        [Fact]
        public void RayDoesDamageOfEnergyType()
        {
            Assert.Equal("acid", ray.DamageType);
        }

        [Fact]
        public void RayDoesDamageBasedOnLevelsOfSorcerer()
        {
            sorcerer.SetLevel(10);
            Assert.Equal("1d6+5", ray.Damage.ToString());
        }

        [Fact]
        public void AttackBonusIsBasedOnRangedAttack()
        {
            sorcerer.Offense.BaseAttackBonus.SetValue(3);
            Assert.Equal(ray.AttackBonus.TotalValue, sorcerer.Offense.RangeAttackBonus.TotalValue);
        }

        [Fact]
        public void UsesPerDayIsBasedOnCharisma()
        {
            Assert.Equal(3, ray.UsesPerDay);
            sorcerer.AbilityScores.SetScore(AbilityScoreTypes.Charisma, 16);
            Assert.Equal(6, ray.UsesPerDay);
        }
        [Fact]
        public void MakesAHandyDisplayString()
        {
            Assert.Equal("3/day Elemental Ray +0 (1d6 acid) 30'", ray.DisplayString());
        }
    }
}