// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters {

    using System.Collections.Generic;
    using System.Linq;
    using Xunit;
    using SilverNeedle;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Equipment;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    
    public class DefenseStatsTests : RequiresDataFiles
    {
        CharacterSheet character;
        DefenseStats basicStats;
        DefenseStats emptyStats;

        public DefenseStatsTests() {
            character = CharacterTestTemplates.AverageBob();
            character.AbilityScores.SetScore (AbilityScoreTypes.Strength, 16);
            character.AbilityScores.SetScore (AbilityScoreTypes.Dexterity, 16);
            character.AbilityScores.SetScore (AbilityScoreTypes.Constitution, 9);
            character.AbilityScores.SetScore (AbilityScoreTypes.Wisdom, 12);
            character.Add(GatewayProvider.Find<Size>("small"));
            character.Size.SetSize(CharacterSize.Small, 1, 1);

            basicStats = character.Get<DefenseStats>();

            var emptyCharacter = CharacterTestTemplates.AverageBob();
            emptyStats = emptyCharacter.Get<DefenseStats>();
        }

        [Fact]
        public void ACIsBasedOnDexterityAndSize() {
            Assert.Equal (14, basicStats.ArmorClass.TotalValue);
        }

        [Fact]
        public void TouchACIsBasedOnDexterityAndSize() {
            Assert.Equal (14, basicStats.TouchArmorClass.TotalValue); }

        [Fact]
        public void FlatFootedACIsBaseACAndSize() {
            Assert.Equal (11, basicStats.FlatFootedArmorClass.TotalValue); }

        [Fact]
        public void ReflexSavingThrowIsBasedOnDexterity() {
            Assert.Equal (3, basicStats.ReflexSave.TotalValue); }

        [Fact]
        public void FortitudeSavingThrowIsBasedOnConstitution() {
            Assert.Equal (-1, basicStats.FortitudeSave.TotalValue);
        }

        [Fact]
        public void WillSavingThrowIsBasedOnWisdom() {
            Assert.Equal (1, basicStats.WillSave.TotalValue);
        }

        [Fact]
        public void MarkingASaveGoodGivesItAPlus2Bonus() {
            Assert.Equal (3, basicStats.ReflexSave.TotalValue);
            basicStats.SetReflexGoodSave ();
            Assert.Equal (5, basicStats.ReflexSave.TotalValue);

            basicStats.SetFortitudeGoodSave ();
            Assert.Equal (1, basicStats.FortitudeSave.TotalValue);

            basicStats.SetWillGoodSave ();
            Assert.Equal (3, basicStats.WillSave.TotalValue);
        }

        [Fact]
        public void SettingGoodSaveRepeatedlyDoesntBoostSave() {
            basicStats.SetReflexGoodSave ();
            basicStats.SetReflexGoodSave ();
            basicStats.SetReflexGoodSave ();

            Assert.Equal (5, basicStats.ReflexSave.TotalValue);
        }

        [Fact]
        public void LevelingUpAClassMarksGoodSaves() {
            var fighter = Class.CreateForTesting();
            fighter.WillSaveRate = Class.PoorSaveRate;
            fighter.FortitudeSaveRate = Class.GoodSaveRate;
            fighter.ReflexSaveRate = Class.PoorSaveRate;

            basicStats.LevelUpDefenseStats (fighter);

            Assert.Equal (1, basicStats.FortitudeSave.TotalValue);
            Assert.Equal (3, basicStats.ReflexSave.TotalValue);
            Assert.Equal (1, basicStats.WillSave.TotalValue);
        }

        [Fact]
        public void LevelingUpMultipleTimesIncreasesTheSaveStats() {
            var fighter = Class.CreateForTesting();
            fighter.WillSaveRate = Class.PoorSaveRate;
            fighter.FortitudeSaveRate = Class.GoodSaveRate;
            fighter.ReflexSaveRate = Class.PoorSaveRate;

            basicStats.LevelUpDefenseStats (fighter);
            basicStats.LevelUpDefenseStats (fighter);
            basicStats.LevelUpDefenseStats (fighter);

            Assert.Equal (3, basicStats.FortitudeSave.TotalValue);
            Assert.Equal (4, basicStats.ReflexSave.TotalValue);
            Assert.Equal (2, basicStats.WillSave.TotalValue);
        }


        [Fact]
        public void EquippedArmorIncreasesYourDefenseAndYourFlatFootedDefenseButNotTouchDefense() {

            var def = character.Defense;
            var startAC = def.ArmorClass.TotalValue;
            var startFlat = def.FlatFootedArmorClass.TotalValue; 
            var startTouch = def.TouchArmorClass.TotalValue; 

            var armor = new Armor ();
            armor.ArmorClass = 10;
            armor.MaximumDexterityBonus = 100;

            character.Inventory.EquipItem (armor);
            Assert.Equal (startAC + 10, def.ArmorClass.TotalValue);
            Assert.Equal (startFlat + 10, def.FlatFootedArmorClass.TotalValue);
            Assert.Equal (startTouch, def.TouchArmorClass.TotalValue);
        }

        [Fact]
        public void CanAddArmorProficiency() {
            emptyStats.AddArmorProficiency("Light");
            Assert.True(emptyStats.IsProficient(Leather()));
        }

        [Fact]
        public void CanAddArmorProficiencies() {
            emptyStats.AddArmorProficiencies(new string[] { "Light", "Heavy" });
            Assert.True(emptyStats.IsProficient(Leather()));
        }

        [Fact]
        public void ArmorCanLimitTheMaxDexterityBonus()
        {
            character.AbilityScores.SetScore(AbilityScoreTypes.Dexterity, 18);
            var armor = new Armor();
            armor.MaximumDexterityBonus = 1;
            character.Inventory.EquipItem(armor);

            Assert.Equal(character.Defense.MaxDexterityBonus.TotalValue, 1);
            Assert.Equal(character.Defense.ArmorClass.TotalValue, 12); //Character is small && dexterity
        }

        [Fact]
        public void CanHaveDamageResistance()
        {
            var dr = new EnergyResistance(5, "-");
            basicStats.AddDamageResistance(dr);
            Assert.Contains(dr, basicStats.EnergyResistance);
        }

        class MockMod : IModifiesStats {
        public IList<IValueStatModifier> Modifiers { get; set;  }

        public MockMod() {
            Modifiers = new List<IValueStatModifier>();
            Modifiers.Add(new ValueStatModifier("Armor Class", 1, "Cause"));
            Modifiers.Add(new ValueStatModifier("Will", 1, "Halfing Luck"));
            Modifiers.Add(new ValueStatModifier("Reflex", 1, "Halfing Luck"));
            Modifiers.Add(new ValueStatModifier("Fortitude", 1, "Halfing Luck"));
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
