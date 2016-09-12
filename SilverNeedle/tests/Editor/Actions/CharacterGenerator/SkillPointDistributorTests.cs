// Copyright (c) 2016 Trevor Redfern
// 
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php

using System;
using NUnit.Framework;
using SilverNeedle.Characters;
using SilverNeedle.Dice;
using System.Linq;
using SilverNeedle;
using System.Collections.Generic;
using SilverNeedle.Actions.CharacterGenerator;
using Moq;

namespace Actions
{
    [TestFixture]
    public class SkillPointDistributorTests
    {
        public List<Skill> availableSkills;
        public SkillRanks skills;
        public SkillPointDistributor distributor;

        [SetUp]
        public void SetUp() 
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

        [Test]
        public void ChoosesSkillsBasedOnTheStrategyRecommendation()
        {
            var strategy = new WeightedOptionTable<string>();
            strategy.AddEntry("Climb", 20);

            distributor.AssignSkillPoints(skills, strategy, 1, 1);      
            Assert.AreEqual(1, skills.GetSkill("Climb").Ranks);
        }

        [Test]
        public void IfSkillHasMaxRanksChooseOtherPreferredSkill()
        {
            var strategy = new WeightedOptionTable<string>();
            strategy.AddEntry("Climb", 2000000);  // This is not a 100% guaranteed test. But it should be reliable enough. If failures happen will adjust
            strategy.AddEntry("Acrobatics", 1);

            distributor.AssignSkillPoints(skills, strategy, 2, 1);
            Assert.AreEqual(1, skills.GetSkill("Climb").Ranks);
            Assert.AreEqual(1, skills.GetSkill("Acrobatics").Ranks);
        }

        [Test]
        public void IfAllPreferredSkillsAreMaxedAssignAPointToRemainingClassSkills()
        {
            var strategy = new WeightedOptionTable<string>();
            strategy.AddEntry("Climb", 1);  
            
            //Set Knowledge (Arcana) as a class skill
            skills.GetSkill("Knowledge (Arcana)").ClassSkill = true;
            
            distributor.AssignSkillPoints(skills, strategy, 2, 1);
            Assert.AreEqual(1, skills.GetSkill("Climb").Ranks);
            Assert.AreEqual(1, skills.GetSkill("Knowledge (Arcana)").Ranks);
            
        }
    }
}