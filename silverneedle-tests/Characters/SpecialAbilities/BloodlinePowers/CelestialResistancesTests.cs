// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities.BloodlinePowers
{
    using System.Linq;
    using Xunit;
    using SilverNeedle.Characters.SpecialAbilities.BloodlinePowers;

    public class CelestialResistancesTests
    {
        [Fact]
        public void AddsAcidAndColdResistance()
        {
            var sorcerer = CharacterTestTemplates.Sorcerer();
            var celestial = new CelestialResistances();
            sorcerer.Add(celestial);
            Assert.True(sorcerer.Defense.DamageResistance.Any(x => x.DamageType == "cold" && x.Amount == 5));
            Assert.True(sorcerer.Defense.DamageResistance.Any(x => x.DamageType == "acid" && x.Amount == 5));
            sorcerer.SetLevel(9);
            //TODO: Hack for damage resistance
            celestial.LeveledUp(sorcerer.Components);
            Assert.True(sorcerer.Defense.DamageResistance.Any(x => x.DamageType == "cold" && x.Amount == 10));
            Assert.True(sorcerer.Defense.DamageResistance.Any(x => x.DamageType == "acid" && x.Amount == 10));
        }
    }
}