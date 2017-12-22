// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters
{
    using System.Linq;
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    public class CharacterFeatureTests
    {
        [Fact]
        public void AddingToACharacterTriggersAddingTheAttributesImmediately()
        {
            var character = CharacterTestTemplates.AverageBob();
            var yaml = @"---
attributes:
  - attribute: 
    name: Test Attr
    items:
      - type: Tests.Characters.CharacterFeatureTests+DummyFeature
        value: 10";
            var feature = new CharacterFeature(yaml.ParseYaml());
            character.Add(feature);
            var dummy = character.Get<DummyFeature>();
            Assert.NotNull(dummy);
            Assert.Equal(10, dummy.Value);
        }

        [Fact]
        public void IfFeatureHasNoAttributesJustLoadEmpty()
        {
            var store = new MemoryStore();
            var feature = new CharacterFeature(store);
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

    }
}