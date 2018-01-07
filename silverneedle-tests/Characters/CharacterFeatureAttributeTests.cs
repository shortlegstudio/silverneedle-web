// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters
{
    using Xunit;
    using SilverNeedle.Actions.CharacterGeneration;
    using SilverNeedle.Characters;
    using SilverNeedle.Serialization;
    using SilverNeedle.Actions;

    public class CharacterFeatureAttributeTests
    {
        [Fact]
        public void RunsAnyCommandsThatMightBeSetToTheAttribute()
        {
            var yaml = @"---
name: Test Commands
commands: 
  - command: Tests.Characters.CharacterFeatureAttributeTests+TestCommand
    extra-data: I RAN";

            var attr = new CharacterFeatureAttribute(yaml.ParseYaml());
            var character = CharacterTestTemplates.AverageBob();
            character.Add(attr);
            Assert.Equal("I RAN", character.Get<string>());
        }

        public class TestCommand : ICharacterFeatureCommand
        {
            string name;
            public TestCommand(IObjectStore configuration)
            {
                name = configuration.GetString("extra-data");
            }
            public void Execute(SilverNeedle.Utility.ComponentContainer character)
            {
                character.Add(name);
            }
        }
    }

}