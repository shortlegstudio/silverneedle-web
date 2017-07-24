// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using NUnit.Framework;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;

    [TestFixture]
    public class DivineBondWeaponTests
    {
        [Test]
        public void GrantsAWeaponBonus()
        {
            var bond = new DivineBondWeapon();
            var character = new CharacterSheet();
            var cls = new Class();
            cls.Name = "Paladin";
            character.SetClass(cls);
            character.SetLevel(13);
            character.Add(bond);
            Assert.That(bond.WeaponBonus, Is.EqualTo(3));
            Assert.That(bond.UsesPerDay, Is.EqualTo(3));
            Assert.That(bond.Name, Is.EqualTo("Divine Bond (Weapon +3, 3/day)"));
        } 
    }
}