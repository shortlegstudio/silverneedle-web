using NUnit.Framework;
using SilverNeedle.Characters;
using SilverNeedle.Dice;
using SilverNeedle.Yaml;

namespace Tests.Characters {

	[TestFixture]
	public class ClassTests {
		[Test]
        public void GetLevelReturnsEmptyLevelIfNothingIsThere()
        {
            var c = new Class();
            var l = c.GetLevel(10);
            Assert.IsNotNull(l);
            Assert.IsInstanceOf(typeof(Level), l);
        }
        Class Fighter;
        Class Monk;
        Class Wizard;

        [SetUp]
        public void SetUp()
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

        [Test]
        public void LoadClassYamlFile()
        {
            Assert.IsNotNull(Fighter);
            Assert.IsNotNull(Monk);
            Assert.IsNotNull(Wizard);
        }

        [Test]
        public void YamlLoadedClassesShouldHaveClassSkills()
        {
            //Validate that the class skills are tracked
            Assert.IsTrue(Fighter.IsClassSkill("Climb"));
            Assert.IsTrue(Fighter.IsClassSkill("Swim"));
            Assert.IsFalse(Fighter.IsClassSkill("Spellcraft"));
        }

        [Test]
        public void YamlLoadedClassesShouldHaveSkillPoints()
        {
            Assert.AreEqual(2, Fighter.SkillPoints);
            Assert.AreEqual(4, Monk.SkillPoints);
        }

        [Test]
        public void YamlLoadedClassesShouldHaveHitDice()
        {
            Assert.AreEqual(DiceSides.d10, Fighter.HitDice);
            Assert.AreEqual(DiceSides.d8, Monk.HitDice);
            Assert.AreEqual(DiceSides.d6, Wizard.HitDice);
        }

        [Test]
        public void ClassesGetAllCraftSkillsIfEnabled()
        {
            Assert.IsTrue(Fighter.IsClassSkill("Craft (Weapons)"));
            Assert.IsTrue(Monk.IsClassSkill("Perform (Dance)"));
            Assert.IsFalse(Fighter.IsClassSkill("Perform (Act)"));
            Assert.IsTrue(Wizard.IsClassSkill("Profession (Farmer)"));

            //Making sure poor implementation is not used
            Assert.IsFalse(Fighter.IsClassSkill("Spellcraft"));
            Assert.IsFalse(Monk.IsClassSkill("Performance Analysis"));
            Assert.IsFalse(Wizard.IsClassSkill("Professional Wrestler"));
        }

        [Test]
        public void ClassesHaveABaseAttackBonusRate()
        {
            Assert.AreEqual(1, Fighter.BaseAttackBonusRate);
            Assert.AreEqual(.75f, Monk.BaseAttackBonusRate);
            Assert.AreEqual(.5f, Wizard.BaseAttackBonusRate);
        }

        [Test]
        public void ClassesHaveAFortitudeSavesRate()
        {
            Assert.AreEqual(0.667f, Fighter.FortitudeSaveRate);
            Assert.AreEqual(0.667f, Monk.FortitudeSaveRate);
            Assert.AreEqual(0.334f, Wizard.FortitudeSaveRate);
        }

        [Test]
        public void ClassesHaveAReflexSavesRate()
        {
            Assert.AreEqual(0.334f, Fighter.ReflexSaveRate);
            Assert.AreEqual(0.667f, Monk.ReflexSaveRate);
            Assert.AreEqual(0.334f, Wizard.ReflexSaveRate);
        }

        [Test]
        public void ClassesHaveAWillSavesRate()
        {
            Assert.AreEqual(0.334f, Fighter.WillSaveRate);
            Assert.AreEqual(0.334f, Monk.WillSaveRate);
            Assert.AreEqual(0.667f, Wizard.WillSaveRate);
        }

        [Test]
        public void ClassHaveArmorProficiencies()
        {
            Assert.IsTrue(Fighter.ArmorProficiencies.Contains("heavy"));
            Assert.IsTrue(Fighter.ArmorProficiencies.Contains("medium"));
            Assert.IsTrue(Fighter.ArmorProficiencies.Contains("light"));
            Assert.IsTrue(Monk.ArmorProficiencies.Count == 0);
            Assert.IsTrue(Wizard.ArmorProficiencies.Count == 0);

        }

        [Test]
        public void ClassKnownTheirGoodSaves()
        {
            Assert.IsTrue(Fighter.IsFortitudeGoodSave);
            Assert.IsFalse(Fighter.IsWillGoodSave);
            Assert.IsFalse(Fighter.IsReflexGoodSave);
            Assert.IsTrue(Wizard.IsWillGoodSave);
            Assert.IsFalse(Wizard.IsFortitudeGoodSave);
            Assert.IsTrue(Monk.IsReflexGoodSave);
        }

        [Test]
        public void ClassesHaveWeaponProficiencies()
        {
            Assert.IsTrue(Fighter.WeaponProficiencies.Contains("martial"));
            Assert.IsTrue(Fighter.WeaponProficiencies.Contains("simple"));
            Assert.IsTrue(Monk.WeaponProficiencies.Contains("monk"));
            Assert.IsTrue(Wizard.WeaponProficiencies.Contains("dagger"));
        }

        [Test]
        public void ClassesDefineHowLongTrainingTakes()
        {
            Assert.AreEqual(ClassDevelopmentAge.Trained, Fighter.ClassDevelopmentAge);
            Assert.AreEqual(ClassDevelopmentAge.Studied, Wizard.ClassDevelopmentAge);
        }

        [Test]
        public void LoadsAbilitiesForLevel() 
        {
            Assert.AreEqual(3, Fighter.Levels.Count);
            Assert.AreEqual(5, Monk.Levels.Count);
            Assert.AreEqual(4, Wizard.Levels.Count);
        }

        [Test]
        public void MatchesOnNameCaseInsensitive()
        {
            Assert.IsTrue(Fighter.Matches("fighter"));
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
  armorproficiencies: light, medium, heavy
  weaponproficiencies: simple, martial
  developedage: Trained
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
  weaponproficiencies: simple, monk
  developedage: Studied
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
  weaponproficiencies: club, dagger, crossbow
  developedage: Studied
  levels:
    - level: 1      
    - level: 2
    - level: 3
    - level: 4
...";
    }
}