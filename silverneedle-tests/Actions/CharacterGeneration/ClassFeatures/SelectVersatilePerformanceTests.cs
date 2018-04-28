// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGeneration.ClassFeatures
{
    using SilverNeedle.Actions.CharacterGeneration.ClassFeatures;
    using SilverNeedle.Characters.SpecialAbilities;
    using Xunit;
    public class SelectVersatilePerformanceTests : RequiresDataFiles
    {
        [Theory]
        [Repeat(100)]
        public void ChoosesTheHighestPerformanceSkillFirstThenSelectsADifferentSkill()
        {
            var bard = CharacterTestTemplates.BardyBard();
            var versatilePerformance = new VersatilePerformance();
            bard.Add(versatilePerformance);
            bard.SkillRanks.GetSkill("Perform (Comedy)").AddRank();
            
            var selector = new SelectVersatilePerformance();
            selector.ExecuteStep(bard);
            AssertExtensions.Contains(bard.SkillRanks.GetSkill("Perform (Comedy)"), versatilePerformance.Skills);

            //Make sure that they are different
            selector.ExecuteStep(bard);
            selector.ExecuteStep(bard);
            AssertExtensions.ListIsUnique(versatilePerformance.Skills);
        }
    }
}