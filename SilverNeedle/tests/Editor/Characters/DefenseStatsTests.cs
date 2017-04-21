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
    using SilverNeedle.Equipment;
    using SilverNeedle.Utility;

    [TestFixture]
    public class DefenseStatsTests {
        DefenseStats smallStats;
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

            smallStats = new DefenseStats ();
            smallStats.Initialize(bag);


            var emptyBag = new ComponentBag();
            emptyBag.Add(new AbilityScores());
            emptyBag.Add(new SizeStats());
            emptyBag.Add(new Inventory());
            emptyStats = new DefenseStats();
            emptyStats.Initialize(emptyBag);
        }

        [Test]
        public void ACIsBasedOnDexterityAndSize() {
            Assert.AreEqual (14, smallStats.ArmorClass());
        }

        [Test]
        public void TouchACIsBasedOnDexterityAndSize() {
            Assert.AreEqual (14, smallStats.TouchArmorClass ()); }

        [Test]
        public void FlatFootedACIsBaseACAndSize() {
            Assert.AreEqual (11, smallStats.FlatFootedArmorClass ()); }

        [Test]
        public void ReflexSavingThrowIsBasedOnDexterity() {
            Assert.AreEqual (3, smallStats.ReflexSave.TotalValue); }

        [Test]
        public void FortitudeSavingThrowIsBasedOnConstitution() {
            Assert.AreEqual (-1, smallStats.FortitudeSave.TotalValue);
        }

        [Test]
        public void WillSavingThrowIsBasedOnWisdom() {
            Assert.AreEqual (1, smallStats.WillSave.TotalValue);
        }

        [Test]
        public void MarkingASaveGoodGivesItAPlus2Bonus() {
            Assert.AreEqual (3, smallStats.ReflexSave.TotalValue);
            smallStats.SetReflexGoodSave ();
            Assert.AreEqual (5, smallStats.ReflexSave.TotalValue);

            smallStats.SetFortitudeGoodSave ();
            Assert.AreEqual (1, smallStats.FortitudeSave.TotalValue);

            smallStats.SetWillGoodSave ();
            Assert.AreEqual (3, smallStats.WillSave.TotalValue);
        }

        [Test]
        public void SettingGoodSaveRepeatedlyDoesntBoostSave() {
            smallStats.SetReflexGoodSave ();
            smallStats.SetReflexGoodSave ();
            smallStats.SetReflexGoodSave ();

            Assert.AreEqual (5, smallStats.ReflexSave.TotalValue);
        }

        [Test]
        public void LevelingUpAClassMarksGoodSaves() {
            var fighter = new Class ();
            fighter.WillSaveRate = Class.PoorSaveRate;
            fighter.FortitudeSaveRate = Class.GoodSaveRate;
            fighter.ReflexSaveRate = Class.PoorSaveRate;

            smallStats.LevelUpDefenseStats (fighter);

            Assert.AreEqual (1, smallStats.FortitudeSave.TotalValue);
            Assert.AreEqual (3, smallStats.ReflexSave.TotalValue);
            Assert.AreEqual (1, smallStats.WillSave.TotalValue);
        }

        [Test]
        public void LevelingUpMultipleTimesIncreasesTheSaveStats() {
            var fighter = new Class ();
            fighter.WillSaveRate = Class.PoorSaveRate;
            fighter.FortitudeSaveRate = Class.GoodSaveRate;
            fighter.ReflexSaveRate = Class.PoorSaveRate;

            smallStats.LevelUpDefenseStats (fighter);
            smallStats.LevelUpDefenseStats (fighter);
            smallStats.LevelUpDefenseStats (fighter);

            Assert.AreEqual (3, smallStats.FortitudeSave.TotalValue);
            Assert.AreEqual (4, smallStats.ReflexSave.TotalValue);
            Assert.AreEqual (2, smallStats.WillSave.TotalValue);
        }


        [Test]
        public void EquippedArmorIncreasesYourDefenseAndYourFlatFootedDefenseButNotTouchDefense() {
            var bag = new ComponentBag();
            var inventory = new Inventory ();
            bag.Add(inventory);
            bag.Add(new AbilityScores());
            bag.Add(new SizeStats());
            var def = new DefenseStats ();
            def.Initialize(bag);
            var startAC = def.ArmorClass();
            var startFlat = def.FlatFootedArmorClass ();
            var startTouch = def.TouchArmorClass ();

            var armor = new Armor ();
            armor.ArmorClass = 10;

            inventory.AddGear (armor);
            inventory.EquipItem (armor);
            Assert.AreEqual (startAC + 10, def.ArmorClass());
            Assert.AreEqual (startFlat + 10, def.FlatFootedArmorClass ());
            Assert.AreEqual (startTouch, def.TouchArmorClass ());
        }


        [Test]
        public void ModifiersCanBeAppliedToArmorClass() {
            var ac = emptyStats.ArmorClass();
            emptyStats.ProcessModifier(new MockMod());
            Assert.AreEqual(ac + 1, emptyStats.ArmorClass());
        }

        [Test]
        public void ModifiersCanBeAppliedToSavingsThrows() {
            var will = emptyStats.WillSave.TotalValue;
            var fort = emptyStats.FortitudeSave.TotalValue;
            var reflex = emptyStats.ReflexSave.TotalValue;
            emptyStats.ProcessModifier(new MockMod());
            Assert.AreEqual(will + 1, emptyStats.WillSave.TotalValue);
            Assert.AreEqual(fort + 1, emptyStats.FortitudeSave.TotalValue);
            Assert.AreEqual(reflex + 1, emptyStats.ReflexSave.TotalValue);
        }

        [Test]
        public void CanAddArmorProficiency() {
            emptyStats.AddArmorProficiency("Light");
            Assert.IsTrue(emptyStats.IsProficient(Leather()));
        }

        [Test]
        public void CanAddArmorProficiencies() {
            emptyStats.AddArmorProficiencies(new string[] { "Light", "Heavy" });
            Assert.IsTrue(emptyStats.IsProficient(Leather()));
        }

        [Test]
        public void CanTrackImmunitiesAndOtherSpecialAbilites() {
            var immune = new ImmunityModifier();
            emptyStats.ProcessSpecialAbilities(immune);

            Assert.AreEqual("vs. Fire", emptyStats.Immunities.First().Condition);
            Assert.AreEqual("Evasion", emptyStats.DefensiveAbilities.First().Condition);
        }

        [Test]
        public void ArmorCanLimitTheMaxDexterityBonus()
        {
            Assert.Ignore("Max Dexterity Bonus Is Not Calculated Yet");
        }

        [Test]
        public void ReturnStatisticsTracked()
        {
            var stats = smallStats.Statistics;
            var statNames = stats.Select(x => x.Name);
            Assert.That(statNames, Is.EquivalentTo(
            new string[] { "Armor Class", "Fortitude Save", "Will Save", "Reflex Save", "Touch Armor Class", "Flat Footed Armor Class" }
            ));
        }

        class MockMod : IModifiesStats {
        public IList<BasicStatModifier> Modifiers { get; set;  }

        public MockMod() {
            Modifiers = new List<BasicStatModifier>();
            Modifiers.Add(new BasicStatModifier("Armor Class", 1, "Cause", "Dodge"));
            Modifiers.Add(new BasicStatModifier("Will", 1, "Halfing Luck", "Trait"));
            Modifiers.Add(new BasicStatModifier("Reflex", 1, "Halfing Luck", "Trait"));
            Modifiers.Add(new BasicStatModifier("Fortitude", 1, "Halfing Luck", "Trait"));
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
