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
        CharacterSheet character;
        SkillRanks Subject;

        public SkillRanksTests()
        {
            character = CharacterTestTemplates.AverageBob();
            character.AbilityScores.SetScore(AbilityScoreTypes.Strength, 14);
            character.AbilityScores.SetScore(AbilityScoreTypes.Dexterity, 12);
            Subject = character.SkillRanks;
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
        public void SettingACraftSkillAsClassSkillSetsAllCraftSkills()
        {
            Subject.SetClassSkill("Craft");
            Assert.True(Subject.GetSkill("Craft (Shoes)").ClassSkill);
            Assert.True(Subject.GetSkill("Craft (Jewelry)").ClassSkill);
        }

        [Fact]
        public void SettingAProfessionsSkillAsClassSkillSetsAllProfessionsSkills()
        {
            Subject.SetClassSkill("Profession");
            Assert.True(Subject.GetSkill("Profession (Fisherman)").ClassSkill);
            Assert.True(Subject.GetSkill("Profession (Brewer)").ClassSkill);
        }

        [Fact]
        public void SettingAPerformSkillAsClassSkillSetsAllPerformSkills()
        {
            Subject.SetClassSkill("Perform");
            Assert.True(Subject.GetSkill("Perform (Oratory)").ClassSkill);
            Assert.True(Subject.GetSkill("Perform (Comedy)").ClassSkill);
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
            var bag = new ComponentContainer();
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
            public IList<IValueStatModifier> Modifiers { get; set; }

            public MockMod()
            {
                Modifiers = new List<IValueStatModifier>();
                Modifiers.Add(new ValueStatModifier("Climb", 3, "Cause"));
            }
        }
    }
}
