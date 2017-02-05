using NUnit.Framework;
using SilverNeedle.Characters;
using SilverNeedle;
using System.IO;
using System.Linq;
using SilverNeedle.Dice;
using SilverNeedle.Yaml;

namespace Characters
{
    [TestFixture]
    public class LevelTests
    {
        private YamlNodeWrapper fighter;
        [SetUp]
        public void SetUp() 
        {
            fighter = fighterLevel.ParseYaml().Children().First();
        }

        [Test]
        public void LevelsKnowTheirNumber() 
        {
            var levelYaml = "level: 2".ParseYaml();
            var level = new Level(levelYaml);
            Assert.AreEqual(2, level.Number);
        }

        [Test]
        public void LevelsCanHaveSpecialAbilities()
        {
            var level = new Level(fighter);
            Assert.AreEqual(1, level.SpecialAbilities.Count);
            var special = level.SpecialAbilities.First();
            Assert.AreEqual("Feat Token", special.Type);
            Assert.AreEqual("combat", special.Condition);
        }

        const string fighterLevel = @"---
- level: 2
  special:
    - name: Bonus Feat
      type: Feat Token
      condition: combat
...";
    }
}