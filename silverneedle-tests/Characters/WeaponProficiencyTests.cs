// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters 
{
    using System;
    using Xunit;
    using SilverNeedle;
    using SilverNeedle.Characters;
    using SilverNeedle.Equipment;
    using SilverNeedle.Serialization;

  
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
        public void NameContainsAListOfWeapons() {
            var config = new MemoryStore();
            config.SetValue("weapons", new string[] { "mace", "sword" });
            var prof = new WeaponProficiency(config);
            Assert.Equal("mace, sword", prof.Name);
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

        [Fact]
        public void CanLoadFromAConfigurationListOfWeaponsAndMatchAllOfThem()
        {
            var config = new MemoryStore();
            config.SetValue("weapons", new string[] { "mace", "sword" });
            var prof = new WeaponProficiency(config);

            var mace = new Weapon();
            mace.Name = "Mace";
            Assert.True(prof.IsProficient(mace));
            var sword = new Weapon();
            sword.Name = "Sword";
            Assert.True(prof.IsProficient(sword));
            var bow = new Weapon();
            bow.Name = "Bow";
            Assert.False(prof.IsProficient(bow));
        }

        [Fact]
        public void SomeRacialProficienciesAreBasedOnWhetherAWordIsInTheName()
        {
            var config = new MemoryStore();
            config.SetValue("weapons", new string[] { "\"dwarven\"" });
            var prof = new WeaponProficiency(config);
            var dwarvenShovel = new Weapon();
            dwarvenShovel.Name = "Ancient Dwarven Shovel of DOOM";
            Assert.True(prof.IsProficient(dwarvenShovel));

            var elvishComb = new Weapon();
            elvishComb.Name = "Handy Elvish Comb of Prettiness";
            Assert.False(prof.IsProficient(elvishComb));
        }
    }
}