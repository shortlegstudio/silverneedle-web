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
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    
    public class CharacterSheetTests : RequiresDataFiles
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
            var sheet = CharacterTestTemplates.AverageBob();
            var fighter = Class.CreateForTesting();
            fighter.SkillPoints = 2;
            sheet.AbilityScores.SetScore(AbilityScoreTypes.Intelligence, 14);
            sheet.SetClass(fighter);
            Assert.Equal(4, sheet.GetSkillPointsPerLevel());
        }

        [Fact]
        public void CanGetAComponentWithADefaultIfMissing()
        {
            var sheet = new CharacterSheet(CharacterStrategy.Default());
            var comp = sheet.GetOrDefault<Occupation>(Occupation.Unemployed());
            Assert.Equal(Occupation.Unemployed(), comp);
        }

        [Fact]
        public void CharactersHaveVitalStats()
        {
            var sheet = new CharacterSheet(CharacterStrategy.Default());
            sheet.FirstName = "Foobar";
            sheet.Alignment = CharacterAlignment.LawfulGood;
            Assert.Equal("Foobar", sheet.Name);
            Assert.Equal(CharacterAlignment.LawfulGood, sheet.Alignment);
        }

        [Fact]
        public void ExposeAllStats()
        {
            var character = CharacterTestTemplates.AverageBob();
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
            var character = new CharacterSheet(CharacterStrategy.Default());
            var ability = new CompAbility();
            character.Add(ability);
            Assert.Equal(character.Components.Get<CompAbility>(), ability);
        }

        [Fact]
        public void AddingAnAbilityThatImplementsIComponentWillCallInitialize()
        {
            var character = new CharacterSheet(CharacterStrategy.Default());
            var ability = new CompAbility();
            character.Add(ability);
            Assert.True(ability.Called);
        }

        [Fact]
        public void CanGetAndSetAlignmentAndItShowsUpAsASingleComponent()
        {
            var character = new CharacterSheet(CharacterStrategy.Default());
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
            var character = new CharacterSheet(CharacterStrategy.Default());
            character.Add(new CompAbility());
            character.Add(new CompAbility());
            character.Add(new CompAbility());

            var abilities = character.GetAndRemoveAll<CompAbility>();
            Assert.Equal(3, abilities.Count());
            Assert.Empty(character.GetAll<CompAbility>());

        }

        [Fact]
        public void CanSaveAndLoadCharacterSheets()
        {
            var bob = CharacterTestTemplates.AverageBob();
            YamlObjectStore store = new YamlObjectStore();
            bob.Alignment = CharacterAlignment.ChaoticEvil;
            bob.Gender = Gender.Male;
            bob.Save(store);

            var yaml = store.WriteToString();
            var loaded = yaml.ParseYaml();

            var bob2 = CharacterSheet.Load(loaded);
            Assert.Equal(bob.Name, bob2.Name);
            Assert.Equal(CharacterAlignment.ChaoticEvil, bob2.Alignment);
            Assert.Equal(Gender.Male, bob2.Gender);
            Assert.NotNull(bob2.Offense);
        }

        [Fact]
        public void HasAGroupOfRequiredStandardComponentsAdded()
        {
            var character = new CharacterSheet(CharacterStrategy.Default());
            Assert.NotNull(character.Get<Inventory>());
            Assert.NotNull(character.Get<OffenseStats>());
            Assert.NotNull(character.Get<DefenseStats>());
            Assert.NotNull(character.Get<SizeStats>());
            Assert.NotNull(character.Get<SilverNeedle.Characters.Attacks.MeleeAttackBonus>());
        }

        public class CompAbility : IComponent
        {
            public bool Called { get; private set; }
            public void Initialize(ComponentContainer bag)
            {
                Called = true;
            }
        }
    }
}
