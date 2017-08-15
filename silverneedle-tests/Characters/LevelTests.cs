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

        private IObjectStore fighter3;
        public LevelTests() 
        {
            fighter = fighterLevel.ParseYaml().Children.First();
            rogue = rogueLevel.ParseYaml().Children.First();
            adept = adeptLevel.ParseYaml().Children.First();
            fighter3 = fighterLevel3.ParseYaml().Children.First();
        }

        [Fact]
        public void LevelsKnowTheirNumber() 
        {
            var levelYaml = "level: 2".ParseYaml();
            var level = new Level(levelYaml);
            Assert.Equal(2, level.Number);
        }

        [Fact]
        public void LevelsCanProvideExtraFeats()
        {
            var level = new Level(fighter);
            Assert.Equal(1, level.FeatTokens.Count);
            var token = level.FeatTokens.First();
            Assert.Contains("combat", token.Tags);
        }

        [Fact]
        public void LevelsCanModifyStats()
        {
            var level = new Level(fighter);
            Assert.Equal(1, level.Modifiers.Count);
            var mod = level.Modifiers.First() as ConditionalStatModifier;
            Assert.Equal("Will", mod.StatisticName);
            Assert.Equal(1, mod.Modifier);
            Assert.Equal("Level (2) Bravery +2", mod.Reason);
            Assert.Equal("bonus", mod.Type);
            Assert.Equal("fear", mod.Condition);
        }

        [Fact]
        public void CanLoadCustomFeatureSteps()
        {
            var level = new Level(adept);
            Assert.Equal(1, level.Steps.Count);
            Assert.IsType<SilverNeedle.Actions.CharacterGeneration.ClassFeatures.ConfigureSummonFamiliar>(level.Steps[0]);
        }

        [Fact]
        public void CustomFeatureStepsCanSupportExtraData()
        {
            var level = new Level(fighter3);
            Assert.Equal(level.Steps.Count, 1);
            var step = level.Steps[0] as SilverNeedle.Actions.CharacterGeneration.ClassFeatures.ConfigureArmorTraining;
            Assert.Equal(step.ArmorTrainingLevel, 1);
        }

        [Fact]
        public void CanSupportAbilities()
        {
            var barb = barbLevel.ParseYaml().Children.First();
            var level = new Level(barb);
            Assert.Equal(level.Abilities.Count, 1);
            Assert.IsType<SilverNeedle.Characters.SpecialAbilities.UncannyDodge>(level.Abilities[0]);
        }
        const string fighterLevel = @"---
- level: 2
  bonus-feats:
    - tags: combat
  modifiers:
    - name: Bravery +2
      stat: Will
      modifier: 1
      type: bonus
      condition: fear
...";

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

        const string fighterLevel3 = @"---
- level: 3
  class-feature-steps:
    - step: SilverNeedle.Actions.CharacterGeneration.ClassFeatures.ConfigureArmorTraining
      level: 1
";
    }
}