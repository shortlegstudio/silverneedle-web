// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities.BloodlinePowers
{
    using System.Linq;
    using Xunit;
    using Moq;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Characters.SpecialAbilities.BloodlinePowers;

    public class DragonResistancesTests
    {
        private DragonResistances resistances;
        private CharacterSheet sorcerer;
        public DragonResistancesTests()
        {
            sorcerer = CharacterTestTemplates.Sorcerer();
            var dragonType = new DragonType();
            dragonType.EnergyType = "cold";
            var mockBloodline = new Mock<IDraconicBloodline>();
            mockBloodline.Setup(x => x.DragonType).Returns(dragonType);
            sorcerer.Add(mockBloodline.Object);

            resistances = new DragonResistances();
            sorcerer.Add(resistances);
        }

        [Fact]
        public void AddDamageResistanceBasedOnEnergyType()
        {
            var defense = sorcerer.Get<DefenseStats>();
            var resist = defense.EnergyResistance.First();
            Assert.Equal("cold", resist.DamageType);
            Assert.Equal(5, resist.Amount);

            sorcerer.SetLevel(9);
            resistances.LeveledUp(sorcerer.Components);
            Assert.Equal(10, resist.Amount);
        }

        [Fact]
        public void AddsNaturalArmorBonus()
        {
            var defense = sorcerer.Get<DefenseStats>();
            Assert.Equal(11, defense.ArmorClass.TotalValue);
            sorcerer.SetLevel(9);
            Assert.Equal(12, defense.ArmorClass.TotalValue);
            sorcerer.SetLevel(15);
            Assert.Equal(14, defense.ArmorClass.TotalValue);
        }
    }
}