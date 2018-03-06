// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Core
{
    using Xunit;
    using SilverNeedle.Actions.CharacterGeneration;
    using SilverNeedle;
    using SilverNeedle.Serialization;
    using SilverNeedle.Actions;

    public class FeatureAttributeTests
    {
        [Fact]
        public void RunsAnyCommandsThatMightBeSetToTheAttribute()
        {
            var yaml = @"---
name: Test Commands
commands: 
  - command: Tests.Core.FeatureAttributeTests+TestCommand
    extra-data: I RAN";

            var attr = new FeatureAttribute(yaml.ParseYaml());
            var character = CharacterTestTemplates.AverageBob();
            character.Add(attr);
            Assert.Equal("I RAN", character.Get<string>());
        }

        public class TestCommand : IFeatureCommand
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