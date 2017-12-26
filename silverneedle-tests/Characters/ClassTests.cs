// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace Tests.Characters {
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Dice;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;
    
    public class ClassTests {
        [Fact]
        public void GetLevelReturnsEmptyLevelIfNothingIsThere()
        {
            var c = new Class();
            var l = c.GetLevel(10);
            Assert.NotNull(l);
            Assert.IsType<Level>(l);
        }
        Class Fighter;
        Class Monk;
        Class Wizard;

        public ClassTests()
        {
            var yaml = ClassYamlFile.ParseYaml();

            foreach(var node in yaml.Children)
            {
                var cls = new Class(node);
                if (cls.Name == "Fighter")
                {
                    Fighter = cls;
                } else if (cls.Name == "Monk") {
                    Monk = cls;
                } else if (cls.Name == "Wizard")
                {
                    Wizard = cls;
                }
            }
        }

        [Fact]
        public void LoadClassYamlFile()
        {
            Assert.NotNull(Fighter);
            Assert.NotNull(Monk);
            Assert.NotNull(Wizard);
        }

        [Fact]
        public void YamlLoadedClassesShouldHaveClassSkills()
        {
            //Validate that the class skills are tracked
            Assert.True(Fighter.IsClassSkill("Climb"));
            Assert.True(Fighter.IsClassSkill("Swim"));
            Assert.False(Fighter.IsClassSkill("Spellcraft"));
        }

        [Fact]
        public void YamlLoadedClassesShouldHaveSkillPoints()
        {
            Assert.Equal(2, Fighter.SkillPoints);
            Assert.Equal(4, Monk.SkillPoints);
        }

        [Fact]
        public void YamlLoadedClassesShouldHaveHitDice()
        {
            Assert.Equal(DiceSides.d10, Fighter.HitDice);
            Assert.Equal(DiceSides.d8, Monk.HitDice);
            Assert.Equal(DiceSides.d6, Wizard.HitDice);
        }

        [Fact]
        public void ClassesGetAllCraftSkillsIfEnabled()
        {
            Assert.True(Fighter.IsClassSkill("Craft (Weapons)"));
            Assert.True(Monk.IsClassSkill("Perform (Dance)"));
            Assert.False(Fighter.IsClassSkill("Perform (Act)"));
            Assert.True(Wizard.IsClassSkill("Profession (Farmer)"));

            //Making sure poor implementation is not used
            Assert.False(Fighter.IsClassSkill("Spellcraft"));
            Assert.False(Monk.IsClassSkill("Performance Analysis"));
            Assert.False(Wizard.IsClassSkill("Professional Wrestler"));
        }

        [Fact]
        public void ClassesHaveABaseAttackBonusRate()
        {
            Assert.Equal(1, Fighter.BaseAttackBonusRate);
            Assert.Equal(.75f, Monk.BaseAttackBonusRate);
            Assert.Equal(.5f, Wizard.BaseAttackBonusRate);
        }

        [Fact]
        public void ClassesHaveAFortitudeSavesRate()
        {
            Assert.Equal(0.667f, Fighter.FortitudeSaveRate);
            Assert.Equal(0.667f, Monk.FortitudeSaveRate);
            Assert.Equal(0.334f, Wizard.FortitudeSaveRate);
        }

        [Fact]
        public void ClassesHaveAReflexSavesRate()
        {
            Assert.Equal(0.334f, Fighter.ReflexSaveRate);
            Assert.Equal(0.667f, Monk.ReflexSaveRate);
            Assert.Equal(0.334f, Wizard.ReflexSaveRate);
        }

        [Fact]
        public void ClassesHaveAWillSavesRate()
        {
            Assert.Equal(0.334f, Fighter.WillSaveRate);
            Assert.Equal(0.334f, Monk.WillSaveRate);
            Assert.Equal(0.667f, Wizard.WillSaveRate);
        }

        [Fact]
        public void ClassHaveArmorProficiencies()
        {
            Assert.True(Fighter.ArmorProficiencies.Contains("heavy"));
            Assert.True(Fighter.ArmorProficiencies.Contains("medium"));
            Assert.True(Fighter.ArmorProficiencies.Contains("light"));
            Assert.True(Monk.ArmorProficiencies.Count == 0);
            Assert.True(Wizard.ArmorProficiencies.Count == 0);

        }

        [Fact]
        public void ClassKnownTheirGoodSaves()
        {
            Assert.True(Fighter.IsFortitudeGoodSave);
            Assert.False(Fighter.IsWillGoodSave);
            Assert.False(Fighter.IsReflexGoodSave);
            Assert.True(Wizard.IsWillGoodSave);
            Assert.False(Wizard.IsFortitudeGoodSave);
            Assert.True(Monk.IsReflexGoodSave);
        }

        [Fact]
        public void ClassesHaveWeaponProficiencies()
        {
            Assert.True(Fighter.WeaponProficiencies.Contains("martial"));
            Assert.True(Fighter.WeaponProficiencies.Contains("simple"));
            Assert.True(Monk.WeaponProficiencies.Contains("monk"));
            Assert.True(Wizard.WeaponProficiencies.Contains("dagger"));
        }

        [Fact]
        public void ClassesDefineHowLongTrainingTakes()
        {
            Assert.Equal(ClassDevelopmentAge.Trained, Fighter.ClassDevelopmentAge);
            Assert.Equal(ClassDevelopmentAge.Studied, Wizard.ClassDevelopmentAge);
        }

        [Fact]
        public void LoadsAbilitiesForLevel() 
        {
            Assert.Equal(3, Fighter.Levels.Count);
            Assert.Equal(5, Monk.Levels.Count);
            Assert.Equal(4, Wizard.Levels.Count);
        }

        [Fact]
        public void MatchesOnNameCaseInsensitive()
        {
            Assert.True(Fighter.Matches("fighter"));
        }

        [Fact]
        public void ClassesCanHaveStartingWealth()
        {
            Assert.Equal(3, Fighter.StartingWealthDice.Dice.Count);
            Assert.Equal(DiceSides.d6, Fighter.StartingWealthDice.Dice[0].Sides);
        }

        [Fact]
        public void ClassesCanHaveCustomBuildStepsForFurtherSpecialization()
        {
            Assert.Equal(Monk.CustomBuildStep, "SilverNeedle.Namespace.ClassName");
        }
        private const string ClassYamlFile = @"--- 
- class: 
  name: Fighter
  skillpoints: 2
  skills: 
    - Climb
    - Swim
    - Craft
    - Profession
  hitdice: d10
  baseattackbonus: 1
  fortitude: 0.667
  reflex: 0.334
  will: 0.334
  armorproficiencies: [light, medium, heavy]
  weaponproficiencies: [simple, martial]
  developedage: Trained
  startingwealth: 3d6
  levels:
    - level: 1      
    - level: 2
    - level: 3
- class: 
  name: Monk
  skillpoints: 4
  skills:
    - Acrobatics
    - Climb
    - Craft
    - Perform
    - Profession
  hitdice: d8
  baseattackbonus: 0.75
  fortitude: 0.667
  reflex: 0.667
  will: 0.334
  weaponproficiencies: [simple, monk]
  developedage: Studied
  startingwealth: 1d6
  custom-build-step: SilverNeedle.Namespace.ClassName
  levels:
    - level: 1      
    - level: 2
    - level: 3
    - level: 4
    - level: 5
- class: 
  name: Wizard
  skillpoints: 4
  skills:
    - Craft
    - Profession
    - Knowledge Arcana
    - Spellcraft
  hitdice: d6
  baseattackbonus: 0.5
  reflex: 0.334
  fortitude: 0.334
  will: 0.667
  weaponproficiencies: [club, dagger, crossbow]
  developedage: Studied 
  levels:
    - level: 1      
    - level: 2
    - level: 3
    - level: 4
...";
    }
}