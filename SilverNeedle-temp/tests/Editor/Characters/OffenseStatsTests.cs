// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters {
    using System.Collections.Generic;
    using System.Linq;
    using NUnit.Framework;
    using SilverNeedle;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Equipment;
    using SilverNeedle.Dice;
    using SilverNeedle.Utility;

    [TestFixture]
    public class OffenseStatsTests {
        OffenseStats smallStats;
        Inventory inventory;

        [SetUp]
        public void SetUp() {
            var abilities = new AbilityScores ();
            abilities.SetScore (AbilityScoreTypes.Strength, 16);
            abilities.SetScore (AbilityScoreTypes.Dexterity, 14);
            var size = new SizeStats (CharacterSize.Small, 1,1);

            inventory = new Inventory();
            smallStats = new OffenseStats ();

            var components = new ComponentBag();
            components.Add(abilities);
            components.Add(size);
            components.Add(smallStats);
            components.Add(inventory);
            smallStats.Initialize(components);
        }

        [Test]
        public void BaseAttackBonusIsAStat() {
            Assert.IsInstanceOf<BasicStat> (smallStats.BaseAttackBonus);
        }

        [Test]
        public void BaseMeleeBonusIsBABAndStrengthAndSize() {
            smallStats.BaseAttackBonus.SetValue (3);
            Assert.AreEqual (7, smallStats.MeleeAttackBonus.TotalValue);
        }

        [Test]
        public void BaseRangeBonusIsBABAndDexterityAndSize() {
            smallStats.BaseAttackBonus.SetValue (3);
            Assert.AreEqual (6, smallStats.RangeAttackBonus.TotalValue);
        }

        [Test]
        public void CMBIsBABAndStrengthAndSize() {
            smallStats.BaseAttackBonus.SetValue (3);
            Assert.AreEqual (5, smallStats.CombatManeuverBonus.TotalValue);
        }

        [Test]
        public void CMDIsBABStrengthAndDexterityAndSize() {
            smallStats.BaseAttackBonus.SetValue (3);
            Assert.AreEqual (17, smallStats.CombatManeuverDefense.TotalValue);
        }

        [Test]
        public void ModifiersCanBeAppliedToCombatManeuverDefense() {
            var mods = new MockMod();
            var oldCMD = smallStats.CombatManeuverDefense.TotalValue;
            smallStats.ProcessModifier(mods);
            Assert.AreEqual(oldCMD + 1, smallStats.CombatManeuverDefense.TotalValue);
        }

        [Test]
        public void ModifiersCanBeAppliedToCombatManeuverBonus() {
            var mods = new MockMod();
            var oldCMB = smallStats.CombatManeuverBonus.TotalValue;
            smallStats.ProcessModifier(mods);
            Assert.AreEqual(oldCMB + 1, smallStats.CombatManeuverBonus.TotalValue);
        }

        [Test]
        public void ContainsAListOfAllWeaponsAvailableAndTheirStats() {
            var longsword = Longsword();
            inventory.AddGear(longsword);
            Assert.AreEqual(1, smallStats.Attacks().Count);
            Assert.AreEqual("Longsword", smallStats.Attacks().First().Name);
            Assert.AreEqual(longsword, smallStats.Attacks().First().Weapon);
        }

        [Test]
        public void MeleeWeaponAttacksCalculateDamageBonuses() {
            inventory.AddGear(Longsword());
            smallStats.AddWeaponProficiency("martial");

            var atk = smallStats.Attacks().First();
            Assert.IsNotNull(atk);
            var diceRoll = atk.Damage;
            Assert.AreEqual(3, diceRoll.Modifier);

            //Should convert damage based on size
            Assert.AreEqual(DiceSides.d6, diceRoll.Dice.First().Sides);
            Assert.AreEqual(smallStats.MeleeAttackBonus.TotalValue, atk.AttackBonus);
        }

        [Test]
        public void RangeAttackBonusHaveAttackBonusButNotDamage() {
            inventory.AddGear(ShortBow());
            smallStats.AddWeaponProficiency("martial");
            var atk = smallStats.Attacks().First();
            Assert.IsNotNull(atk);
            var diceRoll = atk.Damage;
            Assert.AreEqual(0, diceRoll.Modifier);
            Assert.AreEqual(DiceSides.d4, diceRoll.Dice.First().Sides);
            Assert.AreEqual(smallStats.RangeAttackBonus.TotalValue, atk.AttackBonus);
        }

        [Test]
        public void CanAddWeaponProficiencies() {
            smallStats.AddWeaponProficiency("Shortbow");
            Assert.IsTrue(smallStats.IsProficient(ShortBow()));
        }

        [Test]
        public void CanAddAnArrayOfWeaponProficiencies() {
            smallStats.AddWeaponProficiencies(new string[] {"simple", "martial"});
            Assert.IsTrue(smallStats.IsProficient(Longsword()));	
        }

        [Test]
        public void AttacksWithoutProficiencyAreAtMinus4() {
            inventory.AddGear(Nunchaku());
            var atk = smallStats.Attacks().First();
            Assert.IsNotNull(atk);
            Assert.AreEqual(smallStats.MeleeAttackBonus.TotalValue + OffenseStats.UnproficientWeaponModifier, atk.AttackBonus);
        }

        [Test]
        public void ThrownWeaponsAreReturnedForBothMeleeAndRangedVersions()
        {
            inventory.AddGear(Dagger());
            var atks = smallStats.Attacks();
            Assert.IsTrue(atks.Any(x => x.AttackType == AttackTypes.Melee));
            Assert.IsTrue(atks.Any(x => x.AttackType == AttackTypes.Ranged));
        }

        [Test]
        public void TracksSpecialAttackAbilities()
        {
            var special = new SpecialAttacks();
            smallStats.ProcessSpecialAbilities(special);
            Assert.Greater(smallStats.OffensiveAbilities.Count(), 0);
        }

        [Test]
        public void LevelsUpCombatStatsBasedOnClass()
        {
            var cls = new Class();
            cls.BaseAttackBonusRate = 1;
            smallStats.LevelUp(cls);
            Assert.AreEqual(1, smallStats.BaseAttackBonus.TotalValue);
            smallStats.LevelUp(cls);
            Assert.AreEqual(2, smallStats.BaseAttackBonus.TotalValue);            
        }

        [Test]
        public void ReturnsStatsForCombat()
        {
            var stats = smallStats.Statistics;
            Assert.That(stats.Count(), Is.EqualTo(3));
            Assert.That(stats, Is.EquivalentTo(new BasicStat[] { 
                smallStats.BaseAttackBonus, 
                smallStats.CombatManeuverBonus, 
                smallStats.CombatManeuverDefense,
            }));
        }

        [Test]
        public void AllowCustomModifiersToAttackBonusForSpecificWeapons()
        {
            //This allows things like WeaponFocus feats
            var attackMod = new WeaponAttackModifier("Weapon Focus",  1, x => { return x.Name.EqualsIgnoreCase("Longsword"); });
            var damageMod = new WeaponDamageModifier("Weapon Training", 2, x => { return x.Group == WeaponGroup.HeavyBlades; });
            smallStats.AddWeaponProficiency("simple");
            smallStats.AddWeaponProficiency("martial");
            smallStats.AddWeaponModifier(attackMod);
            smallStats.AddWeaponModifier(damageMod);
            inventory.AddGear(Longsword());
            inventory.AddGear(Dagger());

            //Longsword should have bonuses while dagger should not
            var lswordAttack = smallStats.Attacks().First(x => x.Name == "Longsword");
            var dAttack = smallStats.Attacks().First(x => x.Name == "Dagger");

            Assert.That(lswordAttack.AttackBonus, Is.EqualTo(smallStats.MeleeAttackBonus.TotalValue + 1));
            Assert.That(lswordAttack.Damage.Modifier, Is.EqualTo(5));
            Assert.That(dAttack.AttackBonus, Is.EqualTo(smallStats.MeleeAttackBonus.TotalValue));
            Assert.That(dAttack.Damage.Modifier, Is.EqualTo(3));
        }

        [Test]
        public void CanAddSpecialAttacksToStats()
        {
            var attack = new AttackStatistic();
            smallStats.AddAttack(attack);
            Assert.That(smallStats.Attacks(), Contains.Item(attack));
        }

        [Test]
        public void MasterworkWeaponsProvideImprovedAttackBonus()
        {
            var mwkLongsword = new MasterworkWeapon(Longsword());
            inventory.AddGear(mwkLongsword);
            smallStats.AddWeaponProficiency("martial");

            var atk = smallStats.GetAttack(mwkLongsword);
            // Small Size (1) + Str16 (3) + Mwk (1)
            Assert.That(atk.AttackBonus, Is.EqualTo(5));
        }

        private Weapon Longsword() {
            return new Weapon("Longsword", 0, "1d8", DamageTypes.Slashing, 19, 2, 0, WeaponType.OneHanded, WeaponGroup.HeavyBlades, WeaponTrainingLevel.Martial);
        }

        private Weapon ShortBow() {
            return new Weapon("Shortbow", 0, "1d6", DamageTypes.Piercing, 19, 2, 0, WeaponType.Ranged, WeaponGroup.Bows, WeaponTrainingLevel.Martial);
        }

        private Weapon Nunchaku() {
            return new Weapon("Nunchaku", 0, "1d6", DamageTypes.Bludgeoning, 20, 2, 0, WeaponType.OneHanded, WeaponGroup.Monk, WeaponTrainingLevel.Exotic);
        }

        private Weapon Dagger() {
            return new Weapon("Dagger", 0, "1d4", DamageTypes.Piercing, 20, 2, 10, WeaponType.Light, WeaponGroup.Thrown, WeaponTrainingLevel.Simple);
        }

        class MockMod : IModifiesStats {
            public IList<IStatModifier> Modifiers { get; set;  }

            public MockMod() {
                Modifiers = new List<IStatModifier>();
                Modifiers.Add(new ValueStatModifier("CMD", 1, "racial", "Trait"));
                Modifiers.Add(new ValueStatModifier("CMB", 1, "racial", "Trait"));
            }
        }

        class SpecialAttacks : IProvidesSpecialAbilities 
        {
            public IList<SpecialAbility> SpecialAbilities { get; set; }

            public SpecialAttacks() {
                SpecialAbilities = new List<SpecialAbility>();
                SpecialAbilities.Add(new SpecialAbility("Sneak Attack 1d6", "Offensive"));

            }
        }
    }
}
