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
        private IObjectStore rogue;
        private IObjectStore adept;

        public LevelTests() 
        {
            fighter = fighterLevel.ParseYaml();
            rogue = rogueLevel.ParseYaml().Children.First();
            adept = adeptLevel.ParseYaml().Children.First();
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

        [Fact]
        public void CanLoadCustomFeatureSteps()
        {
            var level = new Level(adept);
            Assert.Equal(1, level.Steps.Count);
            Assert.IsType<SilverNeedle.Actions.CharacterGeneration.ClassFeatures.ConfigureSummonFamiliar>(level.Steps[0]);
        }

        [Fact]
        public void CanSupportAbilities()
        {
            var barb = barbLevel.ParseYaml().Children.First();
            var level = new Level(barb);
            Assert.Equal(level.Abilities.Count(), 1);
            Assert.IsType<SilverNeedle.Characters.SpecialAbilities.UncannyDodge>(level.Abilities.First());
        }

        [Fact]
        public void AbilitiesReturnANewInstanceEachTime()
        {
            var barb = barbLevel.ParseYaml().Children.First();
            var level = new Level(barb);
            var abilityOne = level.Abilities.First();
            var abilityTwo = level.Abilities.First();
            Assert.NotEqual(abilityOne, abilityTwo);

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

        const string rogueLevel = @"---
- level: 2
  special:
    - name: Rogue Talent 1
      type: Rogue Talent
      implementation: SilverNeedle.Characters.RogueTalent
      condition:
      level: 1
";

        const string adeptLevel = @"---
- level: 2
  class-feature-steps:
    - step: SilverNeedle.Actions.CharacterGeneration.ClassFeatures.ConfigureSummonFamiliar
";
        const string barbLevel = @"---
- level: 3
  abilities:
    - ability: SilverNeedle.Characters.SpecialAbilities.UncannyDodge";

    }
}