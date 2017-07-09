// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Equipment
{
    using NUnit.Framework;
    using SilverNeedle.Equipment;
    using SilverNeedle.Treasure;

    [TestFixture]
    public class MagicWeaponTests
    {
        [Test]
        public void PlusOneMagicItemsAddToHitAndDamage()
        {
            var longsword = new Weapon("Longsword", 5, "1d8", 
                DamageTypes.Slashing, 20, 2, 0, WeaponType.OneHanded, 
                WeaponGroup.HeavyBlades, WeaponTrainingLevel.Martial);
        
            var magicSword = new MagicWeapon(longsword, 1);
            Assert.That(magicSword.Name, Is.EqualTo("Longsword +1"));
            Assert.That(magicSword.AttackModifier, Is.EqualTo(1));
            Assert.That(magicSword.Damage, Is.EqualTo("1d8+1"));
        }

        [Test]
        public void AddingAMagicDecoratorIncreasesValueBasedOnModifier()
        {
            var sword = new Weapon();
            var magicSword = new MagicWeapon(sword, 2);
            Assert.That(magicSword.Value, Is.EqualTo(800000)); //Based on magic-weapon-value.yml
        }
    }
}