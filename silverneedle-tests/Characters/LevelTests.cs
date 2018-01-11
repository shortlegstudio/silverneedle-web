// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle;
    using System.Linq;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    
    public class LevelTests : RequiresDataFiles
    {
        private IObjectStore fighter;

        public LevelTests() 
        {
            fighter = fighterLevel.ParseYaml();
        }

        [Fact]
        public void LevelsKnowTheirNumber() 
        {
            var levelYaml = "level: 2".ParseYaml();
            var level = new Level(levelYaml);
            Assert.Equal(2, level.Number);
        }

        [Fact]
        public void LevelsCanModifyStats()
        {
            var level = new Level(fighter);
            var character = CharacterTestTemplates.AverageBob();
            character.Add(level);
            AssertCharacter.HasWillSave(1, "fear", character);
        }

        const string fighterLevel = @"---
level: 2
attributes:
  - attribute: 
    name: Bravery +1
    items:
      - type: SilverNeedle.ConditionalStatModifier
        name: Will
        modifier: 1
        modifier-type: bonus
        condition: fear
";

    }
}