// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using System;
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    public class VersatilePerformanceTests
    {
        [Fact]
        public void CanAddPerformCharacterSkills()
        {
            var bard = CharacterTestTemplates.BardyBard();
            var versatilePerformance = new VersatilePerformance();
            versatilePerformance.AddSkill(bard.SkillRanks.GetSkill("Perform (Oratory)"));
            AssertExtensions.Contains(bard.SkillRanks.GetSkill("Perform (Oratory)"), versatilePerformance.Skills);
        }

        [Fact]
        public void WillOnlyAllowPerformSkillsToBeSelected()
        {
            var bard = CharacterTestTemplates.BardyBard();
            var versatilePerformance = new VersatilePerformance();
            Assert.Throws(typeof(ArgumentException),() => { 
                versatilePerformance.AddSkill(bard.SkillRanks.GetSkill("Bluff"));
            });
        }
    }
}