// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace Tests.Characters
{
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle;
    using SilverNeedle.Equipment;
    using SilverNeedle.Utility;

    
    public class CharacterSheetTests
    {
        List<Skill> _testSkills;

        public CharacterSheetTests()
        {
            _testSkills = new List<Skill>();
            _testSkills.Add(new Skill("Climb", AbilityScoreTypes.Strength, false));
            _testSkills.Add(new Skill("Disable Device", AbilityScoreTypes.Dexterity, true));
            _testSkills.Add(new Skill("Spellcraft", AbilityScoreTypes.Intelligence, true));
        }

        [Fact]
        public void CalculatesSkillPointsBasedOnClassAndIntelligence()
        {
            var sheet = new CharacterSheet();
            var fighter = new Class();
            fighter.SkillPoints = 2;
            sheet.AbilityScores.SetScore(AbilityScoreTypes.Intelligence, 14);
            sheet.SetClass(fighter);
            Assert.Equal(4, sheet.GetSkillPointsPerLevel());
        }

        [Fact]
        public void CharactersHaveVitalStats()
        {
            var sheet = new CharacterSheet();
            sheet.FirstName = "Foobar";
            sheet.Alignment = CharacterAlignment.LawfulGood;
            Assert.Equal("Foobar", sheet.Name);
            Assert.Equal(CharacterAlignment.LawfulGood, sheet.Alignment);
        }

        [Fact]
        public void AssigningClassUpdatesWeaponProficiencies()
        {
            var sheet = new CharacterSheet();
            var fighter = new Class();
            fighter.WeaponProficiencies.Add("martial");
            fighter.WeaponProficiencies.Add("simple");
            sheet.SetClass(fighter);

            var wpn = new Weapon();
            wpn.Level = WeaponTrainingLevel.Martial;
            Assert.True(sheet.Offense.IsProficient(wpn));
        }

        [Fact]
        public void AssigningClassUpdatesArmorProficiencies()
        {
            var sheet = new CharacterSheet();
            var fighter = new Class();
            fighter.ArmorProficiencies.Add("Light");
            fighter.ArmorProficiencies.Add("Medium");
            fighter.ArmorProficiencies.Add("Heavy");
            fighter.ArmorProficiencies.Add("Shields");
            sheet.SetClass(fighter);

            Armor armor = new Armor();
            armor.ArmorType = ArmorType.Heavy;
            Assert.True(sheet.Defense.IsProficient(armor));
        }

        [Fact]
        public void AddingATraitToWillSaveBoostsDefense()
        {
            CharacterSheet sheet = new CharacterSheet();
            var trait = new Trait();
            trait.Modifiers.Add(
                new ValueStatModifier("Will", 10, "Trait", "Cause")
            );
            var oldScore = sheet.Defense.WillSave.TotalValue;
            sheet.Add(trait);
            Assert.Equal(oldScore + 10, sheet.Defense.WillSave.TotalValue);
        }

        [Fact]
        public void AddingATraitWillTriggerAddingImmunities()
        {
            CharacterSheet sheet = new CharacterSheet();
            var trait = new Trait();
            trait.SpecialAbilities.Add(
                new SpecialAbility("vs. Spells", "Immunity"));
            sheet.Add(trait);
            Assert.True(sheet.Defense.Immunities.Count() > 0);
        }

        [Fact]
        public void AddingAFeatCouldAddOffensiveAbilities() 
        {
            CharacterSheet sheet = new CharacterSheet();
            var feat = new Feat();
            feat.SpecialAbilities.Add(
                new SpecialAbility("Sneak Attack 1d6", "Offensive"));
            sheet.Add(feat);
            Assert.True(sheet.Offense.OffensiveAbilities.Count() > 0);
        }

        [Fact]
        public void AddingTraitsCouldAddSpecialQualities()
        {
            CharacterSheet sheet = new CharacterSheet();
            sheet.InitializeComponents();
            var trait = new Trait();
            trait.SpecialAbilities.Add(
                new SpecialAbility("vs. Spells", "Ability"));
            sheet.Add(trait);
            Assert.True(sheet.SpecialQualities.SpecialAbilities.Count() > 0);
        }

        [Fact]
        public void ExposeAllStats()
        {
            var character = new CharacterSheet();
            var ac = character.FindStat("Armor Class");
            var str = character.FindStat("Strength");
            var willSave = character.FindStat(StatNames.WillSave);

            Assert.Equal(ac.Name, "Armor Class");
            Assert.Equal(str.Name, "Strength");
            Assert.Equal(willSave.Name, StatNames.WillSave);
        }

        [Fact]
        public void AddingASpecialAbilityAddsToComponentList()
        {
            var character = new CharacterSheet();
            var ability = new SpecialAbility();
            character.Add(ability);
            Assert.Equal(character.Components.Get<SpecialAbility>(), ability);
        }

        [Fact]
        public void AddingAnAbilityThatImplementsIComponentWillCallInitialize()
        {
            var character = new CharacterSheet();
            var ability = new CompAbility();
            character.Add(ability);
            Assert.True(ability.Called);
        }

        [Fact]
        public void CanGetAndSetAlignmentAndItShowsUpAsASingleComponent()
        {
            var character = new CharacterSheet();
            character.Alignment = CharacterAlignment.Neutral;
            Assert.Equal(character.Alignment, CharacterAlignment.Neutral);
            character.Alignment = CharacterAlignment.LawfulEvil;
            Assert.Equal(character.Alignment, CharacterAlignment.LawfulEvil);
            var alignment = character.Get<CharacterAlignment>();
            Assert.Equal(alignment, CharacterAlignment.LawfulEvil);
            Assert.Equal(character.GetAll<CharacterAlignment>().Count(), 1);
        }

        [Fact]
        public void GetAndRemoveAllReturnsAllOfATypeAndRemovesThem()
        {
            var character = new CharacterSheet();
            character.Add(new CompAbility());
            character.Add(new CompAbility());
            character.Add(new CompAbility());

            var abilities = character.GetAndRemoveAll<CompAbility>();
            Assert.Equal(3, abilities.Count());
            Assert.Empty(character.GetAll<CompAbility>());

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
