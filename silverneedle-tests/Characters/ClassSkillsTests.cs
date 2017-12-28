// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Serialization;

    public class ClassSkillsTests
    {
        [Fact]
        public void MarksTheAppropriateSkillAsClassSkill()
        {
            var yaml = @"---
skills: [Climb, Swim, Diplomacy]";
            var clsSkill = new ClassSkills(yaml.ParseYaml());
            var character = CharacterTestTemplates.AverageBob().FullInitialize();
            character.Add(clsSkill);
            AssertCharacter.HasClassSkill("Climb", character);
            AssertCharacter.HasClassSkill("Swim", character);
            AssertCharacter.HasClassSkill("Diplomacy", character);
        }
    }
}