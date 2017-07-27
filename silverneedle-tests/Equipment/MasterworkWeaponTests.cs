// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Equipment
{
    using Xunit;
    using SilverNeedle.Equipment;

    public class MasterworkWeaponTests
    {
        [Fact]
        public void MasterworkWeaponsProvideAnAttackBonusOfOne()
        {
            var longsword = new Weapon("Longsword", 10, "1d8", DamageTypes.Slashing, 
                20, 2, 0, WeaponType.OneHanded, WeaponGroup.HeavyBlades, 
                WeaponTrainingLevel.Martial);
            var mwkLongsword = new MasterworkWeapon(longsword);
            Assert.Equal(mwkLongsword.Name, "Masterwork Longsword");
            Assert.Equal(mwkLongsword.AttackModifier, 1);
            Assert.Equal(mwkLongsword.Value, 30000); // Value is copper Based
        }

        [Fact]
        public void MasterworkDoubleWeaponsCostSixHundredMore()
        {
            var doubleAxe = new Weapon("Double Axe", 10, "1d8", DamageTypes.Slashing, 
                20, 2, 0, WeaponType.TwoHanded, WeaponGroup.Double, 
                WeaponTrainingLevel.Martial);
            var mwkDoubleAxe = new MasterworkWeapon(doubleAxe);
            Assert.Equal(mwkDoubleAxe.AttackModifier, 1);
            Assert.Equal(mwkDoubleAxe.Value, 60000); //Value is copper based
        }
    }
}