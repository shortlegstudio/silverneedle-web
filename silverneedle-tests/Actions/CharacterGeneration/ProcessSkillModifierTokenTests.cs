// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGeneration
{
    using Xunit;
    using SilverNeedle.Actions.CharacterGeneration;
    using SilverNeedle.Characters;

    public class ProcessSkillModifierTokenTests
    {
        [Fact]
        public void CreatesAModifierToASkillThatMatchesTheToken()
        {
            var character = CharacterTestTemplates.AverageBob().WithSkills(new string[] { "Climb", "Appraise" });
            var modifier = new SkillModifierToken(new string[] { "Climb" }, 2, "trait");
            character.Add(modifier);
            var step = new ProcessSkillModifierToken();
            step.ExecuteStep(character);
            Assert.Equal(2, character.SkillRanks.GetScore("Climb"));

        }
    }
}