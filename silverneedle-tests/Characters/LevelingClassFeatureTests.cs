// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters
{
    using System.Linq;
    using SilverNeedle;
    using SilverNeedle.Characters;
    using SilverNeedle.Serialization;
    using Xunit;

    public class LevelClassFeatureTests
    {
        [Fact]
        public void ProcessesLevelsOnLevelUp()
        {
            var yaml = @"---
name: Feature
levels:
  - level: 1
    name: l1
    attributes:
      - attribute:
        name: Some attribute
  - level: 2
    name: l2
    attributes:
      - attribute:
        name: Not There";
            var feature = new LevelingClassFeature(yaml.ParseYaml());
            var character = CharacterTestTemplates.Cleric();
            character.Add(feature);
            Assert.NotNull(character.Components.GetAll<Feature>().First(x => x.Name == "Some attribute"));
            Assert.Null(character.Components.GetAll<Feature>().FirstOrDefault(x => x.Name == "Not There"));
        }

    }
}