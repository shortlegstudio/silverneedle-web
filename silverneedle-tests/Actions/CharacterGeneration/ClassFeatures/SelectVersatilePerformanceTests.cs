// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGeneration.ClassFeatures
{
    using SilverNeedle.Actions.CharacterGeneration.ClassFeatures;
    using SilverNeedle.Characters.SpecialAbilities;
    using Xunit;
    public class SelectVersatilePerformanceTests
    {
        [Fact]
        [Repeat(100)]
        public void ChoosesTheHighestPerformanceSkillFirstThenSelectsADifferentSkill()
        {
            var bard = CharacterTestTemplates.BardyBard().WithSkills(new string[] { "Perform (Comedy)", "Perform (Percussion)" });
            var versatilePerformance = new VersatilePerformance();
            bard.Add(versatilePerformance);
            bard.SkillRanks.GetSkill("Perform (Comedy)").AddRank();
            
            var selector = new SelectVersatilePerformance();
            selector.ExecuteStep(bard);
            AssertExtensions.Contains(bard.SkillRanks.GetSkill("Perform (Comedy)"), versatilePerformance.Skills);
            selector.ExecuteStep(bard);
            AssertExtensions.Contains(bard.SkillRanks.GetSkill("Perform (Percussion)"), versatilePerformance.Skills);
        }
    }
}