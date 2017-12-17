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
        public void CharacterFeaturesParseOutAnyAttributesWithValues()
        {
            var yaml = @"---
attributes:
  - type: SilverNeedle.SomeType
    value: 10
  - type: SilverNeedle.SomeType2
    range: 30";
            var feature = new CharacterFeature(yaml.ParseYaml());
            Assert.Equal(2, feature.Attributes.Count());
            AssertExtensions.EquivalentLists
            (
                new string[] { "SilverNeedle.SomeType", "SilverNeedle.SomeType2" },
                feature.Attributes.Select(x => x.TypeName)
            );
            Assert.Equal(10, feature.Attributes.First().Configuration.GetInteger("value"));
        }

        [Fact]
        public void AddingToACharacterTriggersAddingTheAttributesImmediately()
        {
            var character = CharacterTestTemplates.AverageBob();
            var yaml = @"---
attributes:
  - type: Tests.Characters.CharacterFeatureTests+DummyFeature
    value: 10";
            var feature = new CharacterFeature(yaml.ParseYaml());
            character.Add(feature);
            Assert.NotNull(character.Get<DummyFeature>());
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