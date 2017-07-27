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
    using SilverNeedle.Utility;

    [TestFixture]
    public class DefenseStatsTests {
        DefenseStats basicStats;
        DefenseStats emptyStats;

        [SetUp]
        public void SetUp() {
            var abilities = new AbilityScores ();
            abilities.SetScore (AbilityScoreTypes.Strength, 16);
            abilities.SetScore (AbilityScoreTypes.Dexterity, 16);
            abilities.SetScore (AbilityScoreTypes.Constitution, 9);
            abilities.SetScore (AbilityScoreTypes.Wisdom, 12);
            var size = new SizeStats (CharacterSize.Small);
            var bag = new ComponentBag();
            bag.Add(abilities);
            bag.Add(size);
            bag.Add(new Inventory());

            basicStats = new DefenseStats ();
            basicStats.Initialize(bag);


            var emptyBag = new ComponentBag();
            emptyBag.Add(new AbilityScores());
            emptyBag.Add(new SizeStats());
            emptyBag.Add(new Inventory());
            emptyStats = new DefenseStats();
            emptyStats.Initialize(emptyBag);
        }

        [Fact]
        public void ACIsBasedOnDexterityAndSize() {
            Assert.AreEqual (14, basicStats.ArmorClass.TotalValue);
        }

        [Fact]
        public void TouchACIsBasedOnDexterityAndSize() {
            Assert.AreEqual (14, basicStats.TouchArmorClass.TotalValue); }

        [Fact]
        public void FlatFootedACIsBaseACAndSize() {
            Assert.AreEqual (11, basicStats.FlatFootedArmorClass.TotalValue); }

        [Fact]
        public void ReflexSavingThrowIsBasedOnDexterity() {
            Assert.AreEqual (3, basicStats.ReflexSave.TotalValue); }

        [Fact]
        public void FortitudeSavingThrowIsBasedOnConstitution() {
            Assert.AreEqual (-1, basicStats.FortitudeSave.TotalValue);
        }

        [Fact]
        public void WillSavingThrowIsBasedOnWisdom() {
            Assert.AreEqual (1, basicStats.WillSave.TotalValue);
        }

        [Fact]
        public void MarkingASaveGoodGivesItAPlus2Bonus() {
            Assert.AreEqual (3, basicStats.ReflexSave.TotalValue);
            basicStats.SetReflexGoodSave ();
            Assert.AreEqual (5, basicStats.ReflexSave.TotalValue);

            basicStats.SetFortitudeGoodSave ();
            Assert.AreEqual (1, basicStats.FortitudeSave.TotalValue);

            basicStats.SetWillGoodSave ();
            Assert.AreEqual (3, basicStats.WillSave.TotalValue);
        }

        [Fact]
        public void SettingGoodSaveRepeatedlyDoesntBoostSave() {
            basicStats.SetReflexGoodSave ();
            basicStats.SetReflexGoodSave ();
            basicStats.SetReflexGoodSave ();

            Assert.AreEqual (5, basicStats.ReflexSave.TotalValue);
        }

        [Fact]
        public void LevelingUpAClassMarksGoodSaves() {
            var fighter = new Class ();
            fighter.WillSaveRate = Class.PoorSaveRate;
            fighter.FortitudeSaveRate = Class.GoodSaveRate;
            fighter.ReflexSaveRate = Class.PoorSaveRate;

            basicStats.LevelUpDefenseStats (fighter);

            Assert.AreEqual (1, basicStats.FortitudeSave.TotalValue);
            Assert.AreEqual (3, basicStats.ReflexSave.TotalValue);
            Assert.AreEqual (1, basicStats.WillSave.TotalValue);
        }

        [Fact]
        public void LevelingUpMultipleTimesIncreasesTheSaveStats() {
            var fighter = new Class ();
            fighter.WillSaveRate = Class.PoorSaveRate;
            fighter.FortitudeSaveRate = Class.GoodSaveRate;
            fighter.ReflexSaveRate = Class.PoorSaveRate;

            basicStats.LevelUpDefenseStats (fighter);
            basicStats.LevelUpDefenseStats (fighter);
            basicStats.LevelUpDefenseStats (fighter);

            Assert.AreEqual (3, basicStats.FortitudeSave.TotalValue);
            Assert.AreEqual (4, basicStats.ReflexSave.TotalValue);
            Assert.AreEqual (2, basicStats.WillSave.TotalValue);
        }


        [Fact]
        public void EquippedArmorIncreasesYourDefenseAndYourFlatFootedDefenseButNotTouchDefense() {
            var bag = new ComponentBag();
            var inventory = new Inventory ();
            bag.Add(inventory);
            bag.Add(new AbilityScores());
            bag.Add(new SizeStats());
            var def = new DefenseStats ();
            def.Initialize(bag);
            var startAC = def.ArmorClass.TotalValue;
            var startFlat = def.FlatFootedArmorClass.TotalValue; 
            var startTouch = def.TouchArmorClass.TotalValue; 

            var armor = new Armor ();
            armor.ArmorClass = 10;

            inventory.AddGear (armor);
            inventory.EquipItem (armor);
            Assert.AreEqual (startAC + 10, def.ArmorClass.TotalValue);
            Assert.AreEqual (startFlat + 10, def.FlatFootedArmorClass.TotalValue);
            Assert.AreEqual (startTouch, def.TouchArmorClass.TotalValue);
        }


        [Fact]
        public void ModifiersCanBeAppliedToArmorClass() {
            var ac = emptyStats.ArmorClass.TotalValue;
            emptyStats.ProcessModifier(new MockMod());
            Assert.AreEqual(ac + 1, emptyStats.ArmorClass.TotalValue);
        }

        [Fact]
        public void ModifiersCanBeAppliedToSavingsThrows() {
            var will = emptyStats.WillSave.TotalValue;
            var fort = emptyStats.FortitudeSave.TotalValue;
            var reflex = emptyStats.ReflexSave.TotalValue;
            emptyStats.ProcessModifier(new MockMod());
            Assert.AreEqual(will + 1, emptyStats.WillSave.TotalValue);
            Assert.AreEqual(fort + 1, emptyStats.FortitudeSave.TotalValue);
            Assert.AreEqual(reflex + 1, emptyStats.ReflexSave.TotalValue);
        }

        [Fact]
        public void CanAddArmorProficiency() {
            emptyStats.AddArmorProficiency("Light");
            Assert.IsTrue(emptyStats.IsProficient(Leather()));
        }

        [Fact]
        public void CanAddArmorProficiencies() {
            emptyStats.AddArmorProficiencies(new string[] { "Light", "Heavy" });
            Assert.IsTrue(emptyStats.IsProficient(Leather()));
        }

        [Fact]
        public void CanTrackImmunitiesAndOtherSpecialAbilites() {
            var immune = new ImmunityModifier();
            emptyStats.ProcessSpecialAbilities(immune);

            Assert.AreEqual("vs. Fire", emptyStats.Immunities.First().Condition);
            Assert.AreEqual("Evasion", emptyStats.DefensiveAbilities.First().Condition);
        }

        [Fact]
        public void ArmorCanLimitTheMaxDexterityBonus()
        {
            var bag = new ComponentBag();
            var inventory = new Inventory ();
            bag.Add(inventory);
            var abilities = new AbilityScores();
            abilities.SetScore(AbilityScoreTypes.Dexterity, 18);
            bag.Add(abilities);
            bag.Add(new SizeStats());
            var def = new DefenseStats ();
            def.Initialize(bag);

            var armor = new Armor();
            armor.MaximumDexterityBonus = 1;
            inventory.EquipItem(armor);

            Assert.That(def.MaxDexterityBonus.TotalValue, Is.EqualTo(1));
            Assert.That(def.ArmorClass.TotalValue, Is.EqualTo(11));
        }

        [Fact]
        public void ReturnStatisticsTracked()
        {
            var stats = basicStats.Statistics;
            var statNames = stats.Select(x => x.Name);
            Assert.That(statNames, Is.EquivalentTo(
            new string[] { 
                StatNames.BaseArmorClass,
                StatNames.ArmorClass,
                StatNames.FortitudeSave,
                StatNames.WillSave,
                StatNames.ReflexSave,
                StatNames.TouchArmorClass,
                StatNames.FlatFootedArmorClass,
                StatNames.MaxDexterityBonus }
            ));
        }

        [Fact]
        public void CanHaveDamageResistance()
        {
            var dr = new DamageResistance(5, "-");
            basicStats.AddDamageResistance(dr);
            Assert.That(basicStats.DamageResistance, Contains.Item(dr));
        }

        [Fact]
        public void AddingTheSameTypeOfDamageResistanceStacks()
        {
            var dr = new DamageResistance(1, "-");
            basicStats.AddDamageResistance(dr);
            basicStats.AddDamageResistance(dr);
            Assert.That(basicStats.DamageResistance.First().Amount, Is.EqualTo(2));

        }

        class MockMod : IModifiesStats {
        public IList<IStatModifier> Modifiers { get; set;  }

        public MockMod() {
            Modifiers = new List<IStatModifier>();
            Modifiers.Add(new ValueStatModifier("Armor Class", 1, "Cause", "Dodge"));
            Modifiers.Add(new ValueStatModifier("Will", 1, "Halfing Luck", "Trait"));
            Modifiers.Add(new ValueStatModifier("Reflex", 1, "Halfing Luck", "Trait"));
            Modifiers.Add(new ValueStatModifier("Fortitude", 1, "Halfing Luck", "Trait"));
        }
    }

    class ImmunityModifier : IProvidesSpecialAbilities {
        public IList<SpecialAbility> SpecialAbilities { get; set; }

        public ImmunityModifier() 
        {
            SpecialAbilities = new List<SpecialAbility>();
            SpecialAbilities.Add(new SpecialAbility("vs. Fire", "Immunity"));
            SpecialAbilities.Add(new SpecialAbility("Evasion", "Defensive"));
        }
    }

    private Armor Leather() {
        return new Armor(
        "Leather",
        2,
        10,
        6,
        0,
        10,
        ArmorType.Light
        );
        }
    }
}
