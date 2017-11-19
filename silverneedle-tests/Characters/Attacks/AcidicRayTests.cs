// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.Attacks
{
    using Xunit;
    using SilverNeedle.Characters.Attacks;
    using SilverNeedle.Characters;

    public class AcidicRayTests 
    {
        [Fact]
        public void AcidicRayBasicProperties()
        {
            var ray = new AcidicRay();
            Assert.Equal("Acidic Ray", ray.Name);
            Assert.Equal(20, ray.CriticalThreat);
            Assert.Equal(2, ray.CriticalModifier.TotalValue);
            Assert.Equal(0, ray.SaveDC);
            Assert.Equal(AttackTypes.Special, ray.AttackType);
            Assert.Equal(1, ray.NumberOfAttacks);
            Assert.Equal(30, ray.Range);
        }

        [Fact]
        public void SomeRayPropertiesAreBasedOnCharacter()
        {
            var sorcerer = CharacterTestTemplates.Sorcerer();
            var ray = new AcidicRay();
            sorcerer.Add(ray);
            sorcerer.AbilityScores.SetScore(AbilityScoreTypes.Charisma, 16);
            sorcerer.SetLevel(4);
            Assert.Equal(6, ray.UsesPerDay);
            Assert.Equal("1d6+2", ray.Damage.ToString());
            Assert.Equal(0, ray.AttackBonus.TotalValue);
            Assert.Equal("6/day Acidic Ray +0 (1d6+2 acid) 30'", ray.DisplayString());
        }
    }
}