// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Utility;
    using Moq;

    public class SkillModifierTokenTests
    {
        [Fact]
        public void HasSomeSkillsThatCanHaveTheTokenAppliedTo()
        {
            var yaml = @"---
skills: [Appraise, Climb]
modifier: 2
modifier-type: racial";            
            var token = new SkillModifierToken(yaml.ParseYaml());
            var skill = new Skill("Appraise", AbilityScoreTypes.Intelligence, false);
            var somethignElse = new Skill("Foobar", AbilityScoreTypes.Charisma, false);
            Assert.True(token.Qualifies(skill));
            Assert.False(token.Qualifies(somethignElse));
        }

        [Fact]
        public void CanQualifyWithARangeOfSkillsIfNameIsInQuotes()
        {
            var yaml = @"---
skills: [Climb, ""%Craft%""]
modifier: 2
modifier-type: racial";            
            var token = new SkillModifierToken(yaml.ParseYaml());
            var skill = new Skill("Craft (Shoes)", AbilityScoreTypes.Intelligence, false);
            Assert.True(token.Qualifies(skill));
        }
    }
}