using NUnit.Framework;
using SilverNeedle.Characters;
using System.Collections.Generic;
using System.Linq;
using SilverNeedle;
using SilverNeedle.Equipment;

namespace Characters
{

    [TestFixture]
    public class CharacterSheetTests
    {
        List<Skill> _testSkills;

        [SetUp]
        public void SetUp()
        {
            _testSkills = new List<Skill>();
            _testSkills.Add(new Skill("Climb", AbilityScoreTypes.Strength, false));
            _testSkills.Add(new Skill("Disable Device", AbilityScoreTypes.Dexterity, true));
            _testSkills.Add(new Skill("Spellcraft", AbilityScoreTypes.Intelligence, true));
        }

        [Test]
        public void CalculatesSkillPointsBasedOnClassAndIntelligence()
        {
            var sheet = new CharacterSheet(new List<Skill>());
            var fighter = new Class();
            fighter.SkillPoints = 2;
            sheet.AbilityScores.SetScore(AbilityScoreTypes.Intelligence, 14);
            sheet.SetClass(fighter);
            Assert.AreEqual(4, sheet.GetSkillPointsPerLevel());
        }

        [Test]
        public void CharactersHaveVitalStats()
        {
            var sheet = new CharacterSheet(_testSkills);
            sheet.Name = "Foobar";
            sheet.Alignment = CharacterAlignment.LawfulGood;
            Assert.AreEqual("Foobar", sheet.Name);
            Assert.AreEqual(CharacterAlignment.LawfulGood, sheet.Alignment);
            Assert.AreEqual(1, sheet.Level);
        }

        [Test]
        public void AssigningClassUpdatesWeaponProficiencies()
        {
            var sheet = new CharacterSheet(new List<Skill>());
            var fighter = new Class();
            fighter.WeaponProficiencies.Add("martial");
            fighter.WeaponProficiencies.Add("simple");
            sheet.SetClass(fighter);

            var wpn = new Weapon();
            wpn.Level = WeaponTrainingLevel.Martial;
            Assert.IsTrue(sheet.Offense.IsProficient(wpn));
        }

        [Test]
        public void AssigningClassUpdatesArmorProficiencies()
        {
            var sheet = new CharacterSheet(new List<Skill>());
            var fighter = new Class();
            fighter.ArmorProficiencies.Add("Light");
            fighter.ArmorProficiencies.Add("Medium");
            fighter.ArmorProficiencies.Add("Heavy");
            fighter.ArmorProficiencies.Add("Shields");
            sheet.SetClass(fighter);

            Armor armor = new Armor();
            armor.ArmorType = ArmorType.Heavy;
            Assert.IsTrue(sheet.Defense.IsProficient(armor));
        }


        [Test]
        public void AddTraitTriggersModifiedEvent()
        {
            bool called = false;

            CharacterSheet sheet = new CharacterSheet(_testSkills);
            sheet.Modified += (object sender, CharacterSheetEventArgs e) =>
            {
                called = true;
            };

            //Set up the trait
            var trait = new Trait();
            trait.Name = "Elfy";

            sheet.AddTrait(trait, true);

            //Make sure the event was called
            Assert.IsTrue(called);
        }

        [Test]
        public void AddingATraitToWillSaveBoostsDefense()
        {
            CharacterSheet sheet = new CharacterSheet(_testSkills);
            var trait = new Trait();
            trait.Modifiers.Add(
                new BasicStatModifier("Will", 10, "Trait", "Cause")
            );
            var oldScore = sheet.Defense.WillSave.TotalValue;
            sheet.AddTrait(trait);
            Assert.AreEqual(oldScore + 10, sheet.Defense.WillSave.TotalValue);
        }

        [Test]
        public void AddingATraitWillTriggerAddingImmunities()
        {
            CharacterSheet sheet = new CharacterSheet(_testSkills);
            var trait = new Trait();
            trait.SpecialAbilities.Add(
                new SpecialAbility("vs. Spells", "Immunity"));
            sheet.AddTrait(trait);
            Assert.Greater(sheet.Defense.Immunities.Count(), 0);
        }

        [Test]
        public void AddingAFeatCouldAddOffensiveAbilities() 
        {
            CharacterSheet sheet = new CharacterSheet(_testSkills);
            var feat = new Feat();
            feat.SpecialAbilities.Add(
                new SpecialAbility("Sneak Attack 1d6", "Offensive"));
            sheet.AddFeat(feat);
            Assert.Greater(sheet.Offense.OffensiveAbilities.Count(), 0);
        }

        [Test]
        public void AddingTraitsCouldAddSpecialQualities()
        {
            CharacterSheet sheet = new CharacterSheet(_testSkills);
            var trait = new Trait();
            trait.SpecialAbilities.Add(
                new SpecialAbility("vs. Spells", "Ability"));
            sheet.AddTrait(trait);
            Assert.Greater(sheet.SpecialQualities.SpecialAbilities.Count(), 0);
        }

        [Test]
        public void CanGetSkillByName()
        {
            var character = new CharacterSheet(_testSkills);
            Assert.IsNotNull(character.GetSkill("Climb"));
            Assert.AreEqual("Climb", character.GetSkill("Climb").Name);
            Assert.AreEqual("Climb", character.GetSkill("climb").Name);
        }

        [Test]
        public void ExposeAllStats()
        {
            Assert.Ignore("Not fully implemented stat trackers");
            var character = new CharacterSheet();
            var ac = character.FindStat("Armor Class");
            var str = character.FindStat("Strength");
            var willSave = character.FindStat("Will");

            Assert.That(ac.Name, Is.EqualTo("Armor Class"));
            Assert.That(str.Name, Is.EqualTo("Strength"));
            Assert.That(willSave.Name, Is.EqualTo("Will"));
        }
    }
}
