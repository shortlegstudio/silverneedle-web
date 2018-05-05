// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Serialization;
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
            Assert.True(token.Qualifies("Appraise"));
            Assert.False(token.Qualifies("Foobar"));
        }

        [Fact]
        public void CanQualifyWithARangeOfSkillsIfNameIsInQuotes()
        {
            var yaml = @"---
skills: [Climb, ""%Craft%""]
modifier: 2
modifier-type: racial";            
            var token = new SkillModifierToken(yaml.ParseYaml());
            Assert.True(token.Qualifies("Craft (Shoes)"));
        }

        [Fact]
        public void CreatesTheModifierForASkill()
        {
            var token = new SkillModifierToken(new string[] { "Climb" }, 2, "trait");
            var modifier = token.CreateModifier("Climb");

            Assert.Equal("Climb", modifier.StatisticName);
            Assert.Equal(2, modifier.Modifier);
            Assert.Equal("trait", modifier.ModifierType);
        }
    }
}