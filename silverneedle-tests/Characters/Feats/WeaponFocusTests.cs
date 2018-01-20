// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.Feats
{
    using Xunit;
    using SilverNeedle.Characters.Feats;
    using SilverNeedle.Equipment;
    using SilverNeedle.Serialization;

    public class WeaponFocusTests
    {
        [Fact]
        public void ProvidesSomeDefaultNamesThatMakeSense()
        {
            var weapon = new WeaponFocus();
            Assert.Equal("Weapon Focus", weapon.Name);
        }

        [Fact]
        public void ProvidesAUsefulCopyConstructor()
        {
            var weapon = new WeaponFocus();
            var copy = new WeaponFocus(weapon);
            Assert.Equal(weapon, copy);
        }

        [Theory]
        [InlineData("longsword", "Longsword")]
        [InlineData("martial", "Longsword")]
        [InlineData("simple", "Axe")]
        public void AddingToCharacterPicksAWeaponCharacterIsProficientWithForBonus(string proficiency, string weaponName)
        {
            var longsword = new Weapon("Longsword", 1, "1d6", DamageTypes.Slashing, 20, 2, 0, WeaponType.OneHanded, WeaponGroup.HeavyBlades, WeaponTrainingLevel.Martial);
            var axe = new Weapon("Axe", 1, "1d6", DamageTypes.Slashing, 20, 2, 0, WeaponType.OneHanded, WeaponGroup.HeavyBlades, WeaponTrainingLevel.Simple);
            var list = EntityGateway<Weapon>.LoadFromList(new Weapon[] { longsword, axe });
            var weaponFocus = new WeaponFocus(list);

            //Specific exact weapon proficiency
            var bob = CharacterTestTemplates.AverageBob();
            bob.Offense.AddWeaponProficiency(proficiency);
            bob.Add(weaponFocus);
            Assert.Equal(weaponName, weaponFocus.WeaponName);
            Assert.Equal("Weapon Focus (" + weaponName + ")", weaponFocus.Name);
        }

        [Fact]
        public void HavingWeaponFocusIncreasesAttackBonusByOne()
        {
            var longsword = new Weapon("Longsword", 1, "1d6", DamageTypes.Slashing, 20, 2, 0, WeaponType.OneHanded, WeaponGroup.HeavyBlades, WeaponTrainingLevel.Simple);
            var list = EntityGateway<Weapon>.LoadWithSingleItem(longsword);
            var weaponFocus = new WeaponFocus(list);

            var bob = CharacterTestTemplates.AverageBob();
            bob.Offense.AddWeaponProficiency("simple");
            bob.Add(weaponFocus);
            bob.Inventory.EquipItem(longsword);
            var attackStats = bob.Offense.GetAttack(longsword);
            Assert.Equal(1, attackStats.AttackBonus.TotalValue);
            
        }

        [Fact]
        public void FlagAsUnqualifiedIfAlreadyHasWeaponFocus()
        {
            var longsword = new Weapon("Longsword", 1, "1d6", DamageTypes.Slashing, 20, 2, 0, WeaponType.OneHanded, WeaponGroup.HeavyBlades, WeaponTrainingLevel.Simple);
            var list = EntityGateway<Weapon>.LoadWithSingleItem(longsword);
            var weaponFocus = new WeaponFocus(list);
            var bob = CharacterTestTemplates.AverageBob();
            bob.Offense.AddWeaponProficiency("simple");
            bob.Add(weaponFocus);
            var newFocus = new WeaponFocus(list);
            Assert.False(newFocus.IsQualified(bob));
        }

        [Fact]
        public void NeedToBeProficientWithAtLeastOneWeapon()
        {
            var longsword = new Weapon("Longsword", 1, "1d6", DamageTypes.Slashing, 20, 2, 0, WeaponType.OneHanded, WeaponGroup.HeavyBlades, WeaponTrainingLevel.Simple);
            var list = EntityGateway<Weapon>.LoadWithSingleItem(longsword);
            var character = CharacterTestTemplates.AverageBob();
            var weaponFocus = new WeaponFocus(list);
            Assert.False(weaponFocus.IsQualified(character));
        }

    }
}