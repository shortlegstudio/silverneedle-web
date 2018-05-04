// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGeneration.CustomClassSteps
{
    using System.Linq;
    using Xunit;
    using SilverNeedle.Actions.CharacterGeneration.CustomClassSteps;
    using SilverNeedle.Characters;

    
    public class ExpertCustomStepsTests
    {
        [Fact]
        public void SelectsTenSkillsForClassSkills()
        {
            var character = CharacterTestTemplates.AverageBob();
            var subject = new ExpertCustomSteps();
            subject.Execute(character.Components);
            Assert.Equal(character.SkillRanks.GetClassSkills().Count(), 10);
        }
    }
}