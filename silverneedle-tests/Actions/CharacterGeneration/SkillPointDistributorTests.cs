// Copyright (c) 2016 Trevor Redfern
// 
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php

using Xunit;
using SilverNeedle.Characters;
using System.Linq;
using SilverNeedle.Utility;
using System.Collections.Generic;
using SilverNeedle.Actions.CharacterGeneration;


namespace Tests.Actions
{
    
    public class SkillPointDistributorTests
    {
        private CharacterSheet character;
        public SkillPointDistributor distributor;

        public SkillPointDistributorTests() 
        {
            character = CharacterTestTemplates.Paladin();
            distributor = new SkillPointDistributor();
        }

        [Fact]
        public void ChoosesSkillsBasedOnTheStrategyRecommendation()
        {
            character.Strategy.FavoredSkills.AddEntry("Climb", 20);
            character.Class.SkillPoints = 1;
            distributor.ExecuteStep(character);
            Assert.Equal(1, character.SkillRanks.GetSkill("Climb").Ranks);
        }

        [Fact]
        public void IfSkillHasMaxRanksChooseOtherPreferredSkill()
        {
            character.Strategy.FavoredSkills.AddEntry("Climb", 200000);
            character.Strategy.FavoredSkills.AddEntry("Acrobatics", 1);
            character.Class.SkillPoints = 2;

            distributor.ExecuteStep(character);
            Assert.Equal(1, character.SkillRanks.GetSkill("Climb").Ranks);
            Assert.Equal(1, character.SkillRanks.GetSkill("Acrobatics").Ranks);
        }

        [Fact]
        public void IfAllPreferredSkillsAreMaxedAssignAPointToRemainingClassSkills()
        {
            character.Strategy.FavoredSkills.AddEntry("Climb", 1);
            character.SkillRanks.SetClassSkill("Knowledge Arcana");
            character.Class.SkillPoints = 2;
            
            distributor.ExecuteStep(character);
            Assert.Equal(1, character.SkillRanks.GetSkill("Climb").Ranks);
            Assert.Equal(1, character.SkillRanks.GetSkill("Knowledge Arcana").Ranks);
        }
    }
}