using System;
using System.Linq;
using NUnit.Framework;
using SilverNeedle;
using SilverNeedle.Characters;
using SilverNeedle.Yaml;

namespace Characters {
	[TestFixture]
	public class TraitYamlGatewayTests {
		Trait darkvision;
		Trait hardy;
		Trait halflingLuck;
		Trait stoneCunning;
		TraitYamlGateway gateway;

		[SetUp]
		public void SetUp() {
			gateway = new TraitYamlGateway(TraitYamlFile.ParseYaml());
			var traits = gateway.All();
			darkvision = traits.First (x => x.Name == "Darkvision");
			hardy = traits.First (x => x.Name == "Hardy");
			halflingLuck = traits.First (x => x.Name == "Halfling Luck");
			stoneCunning = traits.First(x => x.Name == "Stonecunning");
		}

		[Test]
		public void LoadTraitYamlFile() {
			Assert.IsNotNull (darkvision);
			Assert.IsNotNull (hardy);
			Assert.IsNotNull (halflingLuck);
		}

		[Test]
		public void TraitsHaveADescription() {
			Assert.AreEqual ("See in the dark.", darkvision.Description);
			Assert.AreEqual ("Really tough", hardy.Description);
		}

		[Test]
		public void TraitsCanHaveSkillAdjustments() {
			var modifiers = hardy.Modifiers;
			Assert.AreEqual (2, modifiers.Count);
			var skillAdj = modifiers.First ();
			Assert.AreEqual ("Heal", skillAdj.StatisticName);
			Assert.AreEqual ("racial", skillAdj.Type);
			Assert.AreEqual ("Hardy (trait)", skillAdj.Reason);
			Assert.AreEqual (2, skillAdj.Modifier);

			var flyAdj = modifiers.Last ();
			Assert.AreEqual ("Fly", flyAdj.StatisticName);
			Assert.AreEqual ("racial", flyAdj.Type);
			Assert.AreEqual (4, flyAdj.Modifier);
		}

		[Test]
		public void TraitsCanModifySavingsThrows() {
			var luck = halflingLuck.Modifiers;
			Assert.AreEqual(3, luck.Count);
		}

		[Test]
		public void TraitsCanHaveConditionalModifiers() {
			var conditional = stoneCunning.Modifiers.OfType<ConditionalStatModifier>();
			Assert.AreEqual(1, conditional.Count());
			Assert.AreEqual("Stoneworking", conditional.First().Condition);
		}

		[Test]
		public void TraitsCanHaveTags() {
			Assert.AreEqual("senses", darkvision.Tags.First());
		}

        [Test]
        public void CanMarkSpecialAbilitiesAsWell()
        {
            Assert.Greater(darkvision.SpecialAbilities.Count, 0);
            Assert.AreEqual("Sight", darkvision.SpecialAbilities.First().Type);
            Assert.AreEqual("In Dark", darkvision.SpecialAbilities.First().Condition);
        }

		private const string TraitYamlFile = @"
- trait: 
  name: Darkvision
  description: See in the dark.
  tags: senses
  special:
    - type: Sight
      condition: In Dark
- trait:
  name: Hardy
  description: Really tough
  modifiers:
    - type: racial
      stat: Heal
      modifier: 2
    - type: racial
      stat: Fly
      modifier: 4
- trait:
  name: Stonecunning
  description: Work the stone
  modifiers:
    - type: racial
      stat: Perception
      modifier: 2
      condition: Stoneworking
- trait:
  name: Halfling Luck
  description: Savings throw bonus
  modifiers:
    - type: racial
      stat: Fortitude
      modifier: 1
    - type: racial
      stat: Reflex
      modifier: 1
    - type: racial
      stat: Will
      modifier: 1
...";
	}
}

