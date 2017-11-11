// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters
{
    using Xunit;
    using System.Collections.Generic;
    using System.Linq;
    using SilverNeedle;
    using SilverNeedle.Characters;
    using SilverNeedle.Equipment;
    using SilverNeedle.Utility;

    
    public class SkillRanksTests
    {
        List<Skill> _skillList;
        AbilityScores _abilityScores;

        SkillRanks Subject;

        public SkillRanksTests()
        {
            _skillList = new List<Skill>();
            _skillList.Add(new Skill("Climb", AbilityScoreTypes.Strength, false, "Climb", true));
            _skillList.Add(new Skill("Disable Device", AbilityScoreTypes.Dexterity, true));
            _skillList.Add(new Skill("Stealth", AbilityScoreTypes.Dexterity, false));

            _abilityScores = new AbilityScores();
            _abilityScores.SetScore(AbilityScoreTypes.Strength, 14);
            _abilityScores.SetScore(AbilityScoreTypes.Dexterity, 12);

            Subject = new SkillRanks(_skillList, _abilityScores);

        }

        [Fact]
        public void SkillRanksLoadsAllTheSkills()
        {
            Assert.Equal(2, Subject.GetScore("Climb"));
            Assert.Equal(int.MinValue, Subject.GetScore("Disable Device"));
        }

        [Fact]
        public void CanProcessASkillModifierForModifyingSkills()
        {
            Subject.ProcessModifier(new MockMod());
            Assert.Equal(5, Subject.GetScore("Climb"));
        }

        [Fact]
        public void ReturnsAListOfSkillsThatHaveRanks()
        {
            Subject.GetSkill("Climb").AddRank();
            var list = Subject.GetRankedSkills().ToList();
            Assert.Equal(1, list.Count);
            Assert.Equal("Climb", list[0].Name);
        }

				[Fact]
				public void CaseInsensitiveSearching()
				{
						Assert.Equal("Climb", Subject.GetSkill("climb").Name);
				}

        [Fact]
        public void IfSkillDoesNotExistWhenProcessingAModifierJustLogIt()
        {
            var ranks = new SkillRanks(new List<Skill>(), new AbilityScores());
            ranks.ProcessModifier(new MockMod());
            //Should not throw exception
        }

        [Fact]
        public void SettingACraftSkillAsClassSkillSetsAllCraftSkills()
        {
            _skillList.Add(new Skill("Craft (Shoes)", AbilityScoreTypes.Intelligence, false));
            _skillList.Add(new Skill("Craft (Jewelry)", AbilityScoreTypes.Intelligence, false));
            var ranks = new SkillRanks(_skillList, new AbilityScores());
            ranks.SetClassSkill("Craft");
            Assert.True(ranks.GetSkill("Craft (Shoes)").ClassSkill);
            Assert.True(ranks.GetSkill("Craft (Jewelry)").ClassSkill);
        }

        [Fact]
        public void SettingAProfessionsSkillAsClassSkillSetsAllProfessionsSkills()
        {
            _skillList.Add(new Skill("Profession (Bouncer)", AbilityScoreTypes.Intelligence, false));
            _skillList.Add(new Skill("Profession (Turnip Farmer)", AbilityScoreTypes.Intelligence, false));
            var ranks = new SkillRanks(_skillList, new AbilityScores());
            ranks.SetClassSkill("Profession");
            Assert.True(ranks.GetSkill("Profession (Bouncer)").ClassSkill);
            Assert.True(ranks.GetSkill("Profession (Turnip Farmer)").ClassSkill);
        }

        [Fact]
        public void SettingAPerformSkillAsClassSkillSetsAllPerformSkills()
        {
            _skillList.Add(new Skill("Perform (Debate)", AbilityScoreTypes.Intelligence, false));
            _skillList.Add(new Skill("Perform (Art)", AbilityScoreTypes.Intelligence, false));
            var ranks = new SkillRanks(_skillList, new AbilityScores());
            ranks.SetClassSkill("Perform");
            Assert.True(ranks.GetSkill("Perform (Debate)").ClassSkill);
            Assert.True(ranks.GetSkill("Perform (Art)").ClassSkill);
        }
    
        [Fact]
        public void CanFetchAllTheClassSkills()
        {
            Subject.SetClassSkill("Climb");
            var classSkills = Subject.GetClassSkills();
            Assert.Equal(1, classSkills.Count());
            Assert.Equal("Climb", classSkills.First().Name);
        }

        [Fact]
        public void SkillPointsPerLevelCanHaveBonuses() 
        {
            _abilityScores.SetScore(AbilityScoreTypes.Intelligence, 10);
            
            var trait = new Trait();
            trait.Modifiers.Add(new ValueStatModifier("Skill Points", 1, "bonus", "trait"));
            Subject.ProcessModifier(trait);
            Assert.Equal(1, Subject.BonusSkillPointsPerLevel());
        }

        [Fact]
        public void SkillPointsPerLevelIsBasedOnIntelligence() {
            _abilityScores.SetScore(AbilityScoreTypes.Intelligence, 16);
            Assert.Equal(3, Subject.BonusSkillPointsPerLevel());
        
        }

        [Fact]
        public void ReturnIntMinimumIfTheSkillIsNotFound()
        {
            var value = Subject.GetScore("Rippadiddledoo");
            Assert.Equal(value, int.MinValue);
        }

        [Fact]
        public void WearingArmorIncreasesArmorCheckPenalty()
        {
            var climb = Subject.GetSkill("Climb");
            var startScore = climb.Score();
            var inventory = new Inventory();
            var armor = new Armor();
            armor.ArmorCheckPenalty = -3;
            inventory.EquipItem(armor);
            var bag = new ComponentBag();
            bag.Add(inventory);

            Subject.Initialize(bag);
            Assert.Equal(Subject.ArmorCheckPenalty.TotalValue, -3);
            Assert.Equal(climb.Score(), startScore - 3);
        }

        [Fact]
        public void EnsureThatArmorCheckPenaltyCannotBePositive()
        {
            Assert.Equal(Subject.ArmorCheckPenalty.Maximum, 0);
        }

        [Fact]
        public void ProvidesAccessToTheStatisticsItProvides()
        {
            var stats = Subject.Statistics.Select(x => x.Name);
            Assert.Contains(StatNames.BonusSkillPoints, stats);
            Assert.Contains(StatNames.ArmorCheckPenalty, stats);
        }

        [Fact]
        public void ExposesSkillsAsStatsToBeModified()
        {
            var climbStat = Subject.Statistics.First(x => x.Name == "Climb");
            Assert.NotNull(climbStat);
        }

        [Fact]
        public void GetSkillIsCaseInsensitive()
        {
            var xx = Subject.GetSkill("climb");
            Assert.NotNull(xx);
        }

        class MockMod : IModifiesStats
        {
            public IList<IStatModifier> Modifiers { get; set; }

            public MockMod()
            {
                Modifiers = new List<IStatModifier>();
                Modifiers.Add(new ValueStatModifier("Climb", 3, "Cause", "Climb"));
            }
        }
    }
}
