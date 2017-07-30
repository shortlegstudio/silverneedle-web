// Copyright (c) 2016 Trevor Redfern
// 
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php

using Xunit;
using SilverNeedle.Characters;
using System.Linq;
using SilverNeedle.Utility;
using System.Collections.Generic;
using SilverNeedle.Actions.CharacterGenerator;


namespace Actions
{
    
    public class SkillPointDistributorTests
    {
        public List<Skill> availableSkills;
        public SkillRanks skills;
        public SkillPointDistributor distributor;

        public SkillPointDistributorTests() 
        {
            availableSkills = new List<Skill>();            
            var climb = new Skill("Climb", AbilityScoreTypes.Strength, false);
            var acrobat = new Skill("Acrobatics", AbilityScoreTypes.Dexterity, false);
            var knowledge = new Skill("Knowledge (Arcana)", AbilityScoreTypes.Intelligence, true);

            availableSkills.Add(climb);
            availableSkills.Add(acrobat);
            availableSkills.Add(knowledge);
            skills = new SkillRanks(availableSkills, new AbilityScores());    
            distributor = new SkillPointDistributor();
        }

        [Fact]
        public void ChoosesSkillsBasedOnTheStrategyRecommendation()
        {
            var strategy = new WeightedOptionTable<string>();
            strategy.AddEntry("Climb", 20);

            distributor.AssignSkillPoints(skills, strategy, 1, 1);      
            Assert.Equal(1, skills.GetSkill("Climb").Ranks);
        }

        [Fact]
        public void IfSkillHasMaxRanksChooseOtherPreferredSkill()
        {
            var strategy = new WeightedOptionTable<string>();
            strategy.AddEntry("Climb", 2000000);  // This is not a 100% guaranteed test. But it should be reliable enough. If failures happen will adjust
            strategy.AddEntry("Acrobatics", 1);

            distributor.AssignSkillPoints(skills, strategy, 2, 1);
            Assert.Equal(1, skills.GetSkill("Climb").Ranks);
            Assert.Equal(1, skills.GetSkill("Acrobatics").Ranks);
        }

        [Fact]
        public void IfAllPreferredSkillsAreMaxedAssignAPointToRemainingClassSkills()
        {
            var strategy = new WeightedOptionTable<string>();
            strategy.AddEntry("Climb", 1);  
            
            //Set Knowledge (Arcana) as a class skill
            skills.SetClassSkill("Knowledge (Arcana)");
            
            distributor.AssignSkillPoints(skills, strategy, 2, 1);
            Assert.Equal(1, skills.GetSkill("Climb").Ranks);
            Assert.Equal(1, skills.GetSkill("Knowledge (Arcana)").Ranks);
        }

        [Fact]
        public void IfAssigningToClassSkillsOnlyUseOneCraftSkill()
        {
            var strategy = new WeightedOptionTable<string>();
            
            //Set Knowledge (Arcana) as a class skill
            availableSkills.Add(new Skill("Craft (Pottery)", AbilityScoreTypes.Intelligence, false));
            availableSkills.Add(new Skill("Craft (Jewelry)", AbilityScoreTypes.Intelligence, false));

            //Assign two points, should go all to one skill
            for (int i = 0; i < 1000; i++)
            {
                var skillRanks = new SkillRanks(availableSkills, new AbilityScores());
                skillRanks.SetClassSkill("Craft (Pottery)");
                skillRanks.SetClassSkill("Craft (Jewelry)");
                
                distributor.AssignSkillPoints(skillRanks, strategy, 2, 2);
                Assert.True(skillRanks.GetSkills().Any(x => x.Ranks == 2));            
            }
        }
    }
}