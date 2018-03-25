// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Core
{
    using System.Linq;
    using Xunit;
    using SilverNeedle;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    public class FeatureTests
    {
        [Fact]
        public void AddingToACharacterTriggersAddingTheAttributesImmediately()
        {
            var character = CharacterTestTemplates.AverageBob();
            var yaml = @"---
name: Feature
attributes:
  - attribute: 
    name: Test Attr
    items:
      - type: Tests.Core.FeatureTests+DummyFeature
        value: 10";
            var feature = new Feature(yaml.ParseYaml());
            character.Add(feature);
            var dummy = character.Get<DummyFeature>();
            Assert.NotNull(dummy);
            Assert.Equal(10, dummy.Value);
        }

        [Fact]
        public void IfFeatureHasNoAttributesJustLoadEmpty()
        {
            var store = new MemoryStore();
            store.SetValue("name", "Some Name");
            var feature = new Feature(store);
            Assert.Empty(feature.Attributes);
        }

        public class DummyFeature
        {
            public int Value { get; private set; }
            public DummyFeature(IObjectStore configuration)
            {
                Value = configuration.GetInteger("value");
            }
        }

        [Fact]
        public void RunsAnyCommandsThatMightBeSetToTheAttribute()
        {
            var yaml = @"---
name: Test Commands
commands: 
  - command: Tests.Core.FeatureTests+TestCommand
    extra-data: I RAN";

            var attr = new Feature(yaml.ParseYaml());
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