// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace Tests.Characters
{
    using System.Collections.Generic;
    using System.Linq;
    using NUnit.Framework;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle;
    using SilverNeedle.Equipment;
    using SilverNeedle.Utility;

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

        [Fact]
        public void CalculatesSkillPointsBasedOnClassAndIntelligence()
        {
            var sheet = new CharacterSheet(new List<Skill>());
            var fighter = new Class();
            fighter.SkillPoints = 2;
            sheet.AbilityScores.SetScore(AbilityScoreTypes.Intelligence, 14);
            sheet.SetClass(fighter);
            Assert.AreEqual(4, sheet.GetSkillPointsPerLevel());
        }

        [Fact]
        public void CharactersHaveVitalStats()
        {
            var sheet = new CharacterSheet(_testSkills);
            sheet.FirstName = "Foobar";
            sheet.Alignment = CharacterAlignment.LawfulGood;
            Assert.AreEqual("Foobar", sheet.Name);
            Assert.AreEqual(CharacterAlignment.LawfulGood, sheet.Alignment);
        }

        [Fact]
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

        [Fact]
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

        [Fact]
        public void AddingATraitToWillSaveBoostsDefense()
        {
            CharacterSheet sheet = new CharacterSheet(_testSkills);
            var trait = new Trait();
            trait.Modifiers.Add(
                new ValueStatModifier("Will", 10, "Trait", "Cause")
            );
            var oldScore = sheet.Defense.WillSave.TotalValue;
            sheet.Add(trait);
            Assert.AreEqual(oldScore + 10, sheet.Defense.WillSave.TotalValue);
        }

        [Fact]
        public void AddingATraitWillTriggerAddingImmunities()
        {
            CharacterSheet sheet = new CharacterSheet(_testSkills);
            var trait = new Trait();
            trait.SpecialAbilities.Add(
                new SpecialAbility("vs. Spells", "Immunity"));
            sheet.Add(trait);
            Assert.Greater(sheet.Defense.Immunities.Count(), 0);
        }

        [Fact]
        public void AddingAFeatCouldAddOffensiveAbilities() 
        {
            CharacterSheet sheet = new CharacterSheet(_testSkills);
            var feat = new Feat();
            feat.SpecialAbilities.Add(
                new SpecialAbility("Sneak Attack 1d6", "Offensive"));
            sheet.Add(feat);
            Assert.Greater(sheet.Offense.OffensiveAbilities.Count(), 0);
        }

        [Fact]
        public void AddingTraitsCouldAddSpecialQualities()
        {
            CharacterSheet sheet = new CharacterSheet(_testSkills);
            sheet.InitializeComponents();
            var trait = new Trait();
            trait.SpecialAbilities.Add(
                new SpecialAbility("vs. Spells", "Ability"));
            sheet.Add(trait);
            Assert.Greater(sheet.SpecialQualities.SpecialAbilities.Count(), 0);
        }

        [Fact]
        public void CanGetSkillByName()
        {
            var character = new CharacterSheet(_testSkills);
            Assert.IsNotNull(character.GetSkill("Climb"));
            Assert.AreEqual("Climb", character.GetSkill("Climb").Name);
            Assert.AreEqual("Climb", character.GetSkill("climb").Name);
        }

        [Fact]
        public void ExposeAllStats()
        {
            var character = new CharacterSheet();
            var ac = character.FindStat("Armor Class");
            var str = character.FindStat("Strength");
            var willSave = character.FindStat(StatNames.WillSave);

            Assert.That(ac.Name, Is.EqualTo("Armor Class"));
            Assert.That(str.Name, Is.EqualTo("Strength"));
            Assert.That(willSave.Name, Is.EqualTo(StatNames.WillSave));
        }

        [Fact]
        public void AddingASpecialAbilityAddsToComponentList()
        {
            var character = new CharacterSheet();
            var ability = new SpecialAbility();
            character.Add(ability);
            Assert.That(character.Components.Get<SpecialAbility>(), Is.EqualTo(ability));
        }

        [Fact]
        public void AddingAnAbilityThatImplementsIComponentWillCallInitialize()
        {
            var character = new CharacterSheet();
            var ability = new CompAbility();
            character.Add(ability);
            Assert.That(ability.Called, Is.True);
        }

        [Fact]
        public void CanGetAndSetAlignmentAndItShowsUpAsASingleComponent()
        {
            var character = new CharacterSheet();
            character.Alignment = CharacterAlignment.Neutral;
            Assert.That(character.Alignment, Is.EqualTo(CharacterAlignment.Neutral));
            character.Alignment = CharacterAlignment.LawfulEvil;
            Assert.That(character.Alignment, Is.EqualTo(CharacterAlignment.LawfulEvil));
            var alignment = character.Get<CharacterAlignment>();
            Assert.That(alignment, Is.EqualTo(CharacterAlignment.LawfulEvil));
            Assert.That(character.GetAll<CharacterAlignment>().Count(), Is.EqualTo(1));
        }

        public class CompAbility : SpecialAbility, IComponent
        {
            public bool Called { get; private set; }
            public void Initialize(ComponentBag bag)
            {
                Called = true;
            }
        }
    }
}
