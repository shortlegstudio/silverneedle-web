// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Core;
    using SilverNeedle.Serialization;

    public class EnergyResistanceTests
    {
        [Fact]
        public void EnergyResistanceCanBeCalculated()
        {
            var energyType = new EnergyType();
            energyType.Name = "Fire";
            var dr = new EnergyResistance(
                energyType,
                () => { return 6 * 10; }
            );
            Assert.Equal("Fire", dr.DamageType);
            Assert.Equal(60, dr.Amount);
            Assert.Equal("Fire 60", dr.DisplayString());
        }

        [Fact]
        public void CanBeMarkedAsImmunity()
        {
            var dr = new EnergyResistance(5, "fire");
            dr.SetToImmunity();
            var character = CharacterTestTemplates.AverageBob();
            character.Defense.AddDamageResistance(dr);
            AssertCharacter.IsImmuneTo("fire", character);
            Assert.Empty(character.Defense.EnergyResistance);
        }

        [Fact]
        public void EnergyResistanceOverTenThousandIsEqualToImmunity()
        {
            var dr = new EnergyResistance(10000, "fire");
            var character = CharacterTestTemplates.AverageBob();
            character.Defense.AddDamageResistance(dr);
            AssertCharacter.IsImmuneTo("fire", character);
        }

        [Fact]
        public void EnergyResistanceCanLoadFromAConfigurationFile()
        {
            var yaml = @"---
damage-type: acid
base-value: 5";
            var res = new EnergyResistance(yaml.ParseYaml());
            Assert.Equal("Energy Resistance (acid)", res.Name);
            Assert.Equal("acid", res.DamageType);
            Assert.Equal(5, res.TotalValue);

        }
    }
}