// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities.BloodlinePowers
{
    using System.Linq;
    using Xunit;
    using SilverNeedle.Characters.SpecialAbilities.BloodlinePowers;

    public class DemonResistancesTests
    {
        [Fact]
        public void AddsElectricityResistanceAndBonusToSaves()
        {
            var sorcerer = CharacterTestTemplates.Sorcerer();
            var demon = new DemonResistances();
            sorcerer.Add(demon);
            Assert.Equal(5, sorcerer.Defense.DamageResistance.First().Amount);
            Assert.Equal("electricity", sorcerer.Defense.DamageResistance.First().DamageType);
            Assert.Equal(2, sorcerer.Defense.FortitudeSave.GetConditionalValue("poison"));

            sorcerer.SetLevel(9);
            //TODO: Hack for damage resistance
            demon.LeveledUp(sorcerer.Components);
            Assert.Equal(10, sorcerer.Defense.DamageResistance.First().Amount);
            Assert.Equal(4, sorcerer.Defense.FortitudeSave.GetConditionalValue("poison"));
        }
    }
}