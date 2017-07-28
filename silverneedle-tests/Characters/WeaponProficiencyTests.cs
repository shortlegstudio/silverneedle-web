// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters {
  using System;
  using Xunit;
  using SilverNeedle;
  using SilverNeedle.Characters;
  using SilverNeedle.Equipment;

  
    public class WeaponProficiencyTests {
        [Fact]
        public void SimpleWeaponsAreProficientForSimple() {
            var prof = new WeaponProficiency("simple");
            var wpn = new Weapon();
            wpn.Level = WeaponTrainingLevel.Simple;
            Assert.True(prof.IsProficient(wpn));
            wpn.Level = WeaponTrainingLevel.Martial;
            Assert.False(prof.IsProficient(wpn));
        }

        [Fact]
        public void MartialWeaponsAreProficientIfMartiallyTrained() {
            var prof = new WeaponProficiency("martial");
            var wpn = new Weapon();
            wpn.Level = WeaponTrainingLevel.Martial;
            Assert.True(prof.IsProficient(wpn));
        }

        [Fact]
        public void MatchesBasedOnNameIfNotTrainingLevel() {
            var prof = new WeaponProficiency("Shortbow");
            var wpn = new Weapon();
            wpn.Name = "Longsword";
            Assert.False(prof.IsProficient(wpn));
            wpn.Name = "Shortbow";
            Assert.True(prof.IsProficient(wpn));
        }

        [Fact]
        public void NameLooksHumanReadable() {
            var prof = new WeaponProficiency("simple");
            Assert.Equal("Simple weapons", prof.Name);
        }

        [Fact]
        public void ShouldProperlyRecognizeProficiencyWithMasterworkWeapons()
        {
            var prof = new WeaponProficiency("Shortbow");
            var wpn = new Weapon();
            wpn.Name = "Shortbow";
            var mwkWpn = new MasterworkWeapon(wpn);
            Assert.True(prof.IsProficient(mwkWpn));
        }
    }
}