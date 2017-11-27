// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities.BloodlinePowers
{
    using Xunit;
    using Moq;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Attacks;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Characters.SpecialAbilities.BloodlinePowers;


    public class ClawsTests
    {
        [Fact]
        public void ConfiguresTheAttacksBasedOnAttributesOfTheCharacter()
        {
            var sorcerer = CharacterTestTemplates.Sorcerer();
            var claws = new Claws();
            sorcerer.Add(claws);

            Assert.Equal(2, claws.NumberOfAttacks);
            Assert.Equal(2, claws.CriticalModifier.TotalValue);
            Assert.Equal(20, claws.CriticalThreat);
            Assert.Equal(AttackTypes.Special, claws.AttackType);
            Assert.Equal(0, claws.SaveDC);
            Assert.Equal(0, claws.Range);

            Assert.Equal(3, claws.RoundsPerDay);
            sorcerer.AbilityScores.SetScore(AbilityScoreTypes.Charisma, 16);
            Assert.Equal(6, claws.RoundsPerDay);

            Assert.Equal("1d4", claws.Damage.ToString());
            sorcerer.AbilityScores.SetScore(AbilityScoreTypes.Strength, 16);
            Assert.Equal("1d4+3", claws.Damage.ToString());

            Assert.Equal(3, claws.AttackBonus.TotalValue);

            Assert.Equal("2 claws +3 (1d4+3) 6 rounds/day", claws.DisplayString());
        }

        [Fact]
        public void AttackDamageIsBasedOnSizeOfCharacter()
        {

            var sorcerer = CharacterTestTemplates.Sorcerer();
            var claws = new Claws();
            sorcerer.Add(claws);
            sorcerer.Size.SetSize(CharacterSize.Small, 10, 10);
            Assert.Equal("1d3", claws.Damage.ToString());
        }

        [Fact]
        public void DamageIncreasesAt7ThLevel()
        {
            var sorcerer = CharacterTestTemplates.Sorcerer();
            var claws = new Claws();
            sorcerer.Add(claws);
            sorcerer.SetLevel(7);
            Assert.Equal("1d6", claws.Damage.ToString());
        }

        [Fact]
        public void MagicalAtFifthLevel()
        {
            var sorcerer = CharacterTestTemplates.Sorcerer();
            var claws = new Claws();
            sorcerer.Add(claws);
            sorcerer.SetLevel(5);
            Assert.Contains("magical", claws.DisplayString());
        }

        [Fact]
        public void FireDamageAddedAtEleventh()
        {
            var sorcerer = CharacterTestTemplates.Sorcerer();
            var claws = new Claws();
            sorcerer.Add(claws);
            sorcerer.SetLevel(11);
            Assert.Contains("1d6 fire", claws.DisplayString());
        }

        [Fact]
        public void DraconicBloodlineDoesBonusDamageOfType()
        {
            var sorcerer = CharacterTestTemplates.Sorcerer();
            var draconic = new Mock<IDraconicBloodline>();
            var dragonType = new DragonType();
            dragonType.EnergyType = "cold";
            draconic.Setup(x => x.DragonType).Returns(dragonType);
            sorcerer.Add(draconic.Object);
            
            var claws = new Claws();
            sorcerer.Add(claws);
            sorcerer.SetLevel(11);
            Assert.Contains("1d6 cold", claws.DisplayString());
        }
    }
}