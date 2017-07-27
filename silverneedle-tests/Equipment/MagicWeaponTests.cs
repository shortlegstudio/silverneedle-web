// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Equipment
{
    using Xunit;
    using SilverNeedle.Equipment;
    using SilverNeedle.Treasure;

    public class MagicWeaponTests : RequiresDataFiles
    {
        [Fact]
        public void PlusOneMagicItemsAddToHitAndDamage()
        {
            var longsword = new Weapon("Longsword", 5, "1d8", 
                DamageTypes.Slashing, 20, 2, 0, WeaponType.OneHanded, 
                WeaponGroup.HeavyBlades, WeaponTrainingLevel.Martial);
        
            var magicSword = new MagicWeapon(longsword, 1);
            Assert.Equal(magicSword.Name, "Longsword +1");
            Assert.Equal(magicSword.AttackModifier, 1);
            Assert.Equal(magicSword.Damage, "1d8+1");
        }

        [Fact]
        public void AddingAMagicDecoratorIncreasesValueBasedOnModifier()
        {
            var sword = new Weapon();
            var magicSword = new MagicWeapon(sword, 2);
            Assert.Equal(magicSword.Value, 800000); //Based on magic-weapon-value.yml
        }
    }
}