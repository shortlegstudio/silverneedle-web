// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters
{
    using NUnit.Framework;
    using SilverNeedle.Characters;
    using SilverNeedle;
    using System.Linq;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    [TestFixture]
    public class LevelTests
    {
        private IObjectStore fighter;
        private IObjectStore rogue;
        private IObjectStore adept;

        private IObjectStore fighter3;
        [SetUp]
        public void SetUp() 
        {
            fighter = fighterLevel.ParseYaml().Children.First();
            rogue = rogueLevel.ParseYaml().Children.First();
            adept = adeptLevel.ParseYaml().Children.First();
            fighter3 = fighterLevel3.ParseYaml().Children.First();
        }

        [Test]
        public void LevelsKnowTheirNumber() 
        {
            var levelYaml = "level: 2".ParseYaml();
            var level = new Level(levelYaml);
            Assert.AreEqual(2, level.Number);
        }

        [Test]
        public void LevelsCanProvideExtraFeats()
        {
            var level = new Level(fighter);
            Assert.AreEqual(1, level.FeatTokens.Count);
            var token = level.FeatTokens.First();
            Assert.That(token.Tags, Contains.Item("combat"));
        }

        [Test]
        public void LevelsCanModifyStats()
        {
            var level = new Level(fighter);
            Assert.AreEqual(1, level.Modifiers.Count);
            var mod = level.Modifiers.First() as ConditionalStatModifier;
            Assert.AreEqual("Will", mod.StatisticName);
            Assert.AreEqual(1, mod.Modifier);
            Assert.AreEqual("Level (2) Bravery +2", mod.Reason);
            Assert.AreEqual("bonus", mod.Type);
            Assert.AreEqual("fear", mod.Condition);
        }

        [Test]
        public void CanLoadCustomFeatureSteps()
        {
            var level = new Level(adept);
            Assert.AreEqual(1, level.Steps.Count);
            Assert.That(level.Steps[0], 
                Is.TypeOf(typeof(SilverNeedle.Actions.CharacterGenerator.ClassFeatures.ConfigureSummonFamiliar)));
        }

        [Test]
        public void CustomFeatureStepsCanSupportExtraData()
        {
            var level = new Level(fighter3);
            Assert.That(level.Steps.Count, Is.EqualTo(1));
            var step = level.Steps[0] as SilverNeedle.Actions.CharacterGenerator.ClassFeatures.ConfigureArmorTraining;
            Assert.That(step.ArmorTrainingLevel, Is.EqualTo(1));
        }

        [Test]
        public void CanSupportAbilities()
        {
            var barb = barbLevel.ParseYaml().Children.First();
            var level = new Level(barb);
            Assert.That(level.Abilities.Count, Is.EqualTo(1));
            Assert.That(level.Abilities[0], Is.TypeOf(typeof(SilverNeedle.Characters.SpecialAbilities.UncannyDodge)));
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
    - step: SilverNeedle.Actions.CharacterGenerator.ClassFeatures.ConfigureSummonFamiliar
";
        const string barbLevel = @"---
- level: 3
  abilities:
    - ability: SilverNeedle.Characters.SpecialAbilities.UncannyDodge";

        const string fighterLevel3 = @"---
- level: 3
  class-feature-steps:
    - step: SilverNeedle.Actions.CharacterGenerator.ClassFeatures.ConfigureArmorTraining
      level: 1
";
    }
}