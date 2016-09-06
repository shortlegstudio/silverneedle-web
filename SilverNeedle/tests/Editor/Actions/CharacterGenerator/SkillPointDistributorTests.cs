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
        }

        [Test]
        public void ChoosesSkillsBasedOnTheStrategyRecommendation()
        {
            var distributor = new SkillPointDistributor();
            var character = new CharacterSheet(availableSkills);

            var cls = new Class();
            var strategy = new WeightedOptionTable<string>();
            strategy.AddEntry("Climb", 20);

            var settings = new SkillPointDistributor.DistributionSettings(
                cls,
                strategy
            );
            settings.SkillPointsToAssign = 1;

            distributor.AssignSkillPoints(character, settings);      
            Assert.AreEqual(1, character.GetSkill("Climb").Ranks);
        }
    }
}