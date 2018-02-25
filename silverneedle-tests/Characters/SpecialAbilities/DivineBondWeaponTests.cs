// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;

    
    public class DivineBondWeaponTests
    {
        [Fact]
        public void GrantsAWeaponBonus()
        {
            var bond = new DivineBondWeapon();
            var character = CharacterTestTemplates.Paladin();
            character.SetLevel(13);
            character.Add(bond);
            Assert.Equal(bond.WeaponBonus, 3);
            Assert.Equal(bond.UsesPerDay, 3);
            Assert.Equal(bond.DisplayString(), "Divine Bond (Weapon +3, 3/day)");
        } 
    }
}