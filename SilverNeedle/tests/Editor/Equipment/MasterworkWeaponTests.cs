// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Equipment
{
    using NUnit.Framework;
    using SilverNeedle.Equipment;

    [TestFixture]
    public class MasterworkWeaponTests
    {
        [Test]
        public void MasterworkWeaponsProvideAnAttackBonusOfOne()
        {
            var longsword = new Weapon("Longsword", 10, "1d8", DamageTypes.Slashing, 
                20, 2, 0, WeaponType.OneHanded, WeaponGroup.HeavyBlades, 
                WeaponTrainingLevel.Martial);
            longsword.Value = 20;
            var mwkLongsword = new MasterworkWeapon(longsword);
            Assert.That(mwkLongsword.Name, Is.EqualTo("Masterwork Longsword"));
            Assert.That(mwkLongsword.AttackModifier, Is.EqualTo(1));
            Assert.That(mwkLongsword.Value, Is.EqualTo(320));
        }

        [Test]
        public void MasterworkDoubleWeaponsCostSixHundredMore()
        {
            var doubleAxe = new Weapon("Double Axe", 10, "1d8", DamageTypes.Slashing, 
                20, 2, 0, WeaponType.TwoHanded, WeaponGroup.Double, 
                WeaponTrainingLevel.Martial);
            doubleAxe.Value = 20;
            var mwkDoubleAxe = new MasterworkWeapon(doubleAxe);
            Assert.That(mwkDoubleAxe.AttackModifier, Is.EqualTo(1));
            Assert.That(mwkDoubleAxe.Value, Is.EqualTo(620));
        }
    }
}