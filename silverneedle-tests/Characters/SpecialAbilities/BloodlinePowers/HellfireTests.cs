// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities.BloodlinePowers
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities.BloodlinePowers;

    public class HellfireTests
    {
        [Fact]
        public void HellfireBlastIsBasedOnCharacterStats()
        {
            var sorcerer = CharacterTestTemplates.Sorcerer();
            var hellFire = new Hellfire();
            sorcerer.Add(hellFire);

            sorcerer.SetLevel(10);
            Assert.Equal("10d6", hellFire.Damage.ToString());
            sorcerer.AbilityScores.SetScore(AbilityScoreTypes.Charisma, 16);
            Assert.Equal(18, hellFire.SaveDC);

            Assert.Equal("1/day Hellfire 10' radius (10d6 fire, DC 18)", hellFire.DisplayString());
        }
    }

}