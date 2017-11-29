// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities.BloodlinePowers
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities.BloodlinePowers;

    public class ElementalBlastTests
    {
        [Fact]
        public void ElementalBlastDealsDamageInARadiusDependingOnLevels()
        {
            var sorcerer = CharacterTestTemplates.Sorcerer();
            sorcerer.SetLevel(10);
            var type = new ElementalType();
            type.EnergyType = "acid";
            sorcerer.Add(type);
            var blast = new ElementalBlast();
            sorcerer.Add(blast);

            Assert.Equal(15, blast.SaveDC);
            sorcerer.AbilityScores.SetScore(AbilityScoreTypes.Charisma, 16);
            Assert.Equal(18, blast.SaveDC);

            Assert.Equal("10d6", blast.Damage.ToString());
            Assert.Equal("acid", blast.DamageType);
            Assert.Equal("1/day Elemental Blast 20' radius (10d6 acid, DC 18)", blast.DisplayString());

        }
    }
}