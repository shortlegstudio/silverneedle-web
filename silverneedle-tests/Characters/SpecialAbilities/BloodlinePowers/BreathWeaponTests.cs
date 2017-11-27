// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities.BloodlinePowers
{
    using Xunit;
    using Moq;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Characters.SpecialAbilities.BloodlinePowers;

    public class BreathWeaponTests
    {
        [Fact]
        public void ProvidesDamageBasedOnEnergyType()
        {
            var sorcerer = CharacterTestTemplates.Sorcerer();
            var dragonType = new DragonType();
            dragonType.EnergyType = "cold";
            dragonType.BreathRange = 30;
            dragonType.BreathShape = "cone";
            var draconic = new Mock<IDraconicBloodline>();
            draconic.Setup(x => x.DragonType).Returns(dragonType);
            sorcerer.Add(draconic.Object);

            sorcerer.SetLevel(10);

            var breath = new BreathWeapon();
            sorcerer.Add(breath);

            Assert.Equal("10d6", breath.Damage.ToString());
            Assert.Equal(15, breath.SaveDC);
            sorcerer.AbilityScores.SetScore(AbilityScoreTypes.Charisma, 16);
            Assert.Equal(18, breath.SaveDC);

            Assert.Equal(1, breath.UsesPerDay);
            Assert.Equal("Breath Weapon (30' cone, 10d6 cold, DC 18, 1/day)", breath.DisplayString());
            
        }
    }
}