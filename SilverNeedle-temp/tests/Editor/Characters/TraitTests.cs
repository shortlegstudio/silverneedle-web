using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using SilverNeedle;
using SilverNeedle.Characters;
using SilverNeedle.Utility;

namespace Tests.Characters {

  [TestFixture]
  public class TraitTests {
        Trait darkvision;
    Trait hardy;
    Trait halflingLuck;
    Trait stoneCunning;
        Trait configureTrait;

        [SetUp]
        public void SetUp() 
        {
            var traits = new List<Trait>();
            var data = TraitYamlFile.ParseYaml();
            foreach(var obj in data.Children)
            {
                traits.Add(new Trait(obj));
            }
            darkvision = traits.First (x => x.Name == "Darkvision");
            hardy = traits.First (x => x.Name == "Hardy");
            halflingLuck = traits.First (x => x.Name == "Halfling Luck");
            stoneCunning = traits.First(x => x.Name == "Stonecunning");
            configureTrait = traits.First(x => x.Name == "Configure");
        }

    [Fact]
    public void LoadTraitYamlFile() {
      Assert.IsNotNull (darkvision);
      Assert.IsNotNull (hardy);
      Assert.IsNotNull (halflingLuck);
    }

    [Fact]
    public void TraitsHaveADescription() {
      Assert.AreEqual ("See in the dark.", darkvision.Description);
      Assert.AreEqual ("Really tough", hardy.Description);
    }

    [Fact]
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

		[Fact]
		public void TraitsCanModifySavingsThrows() {
			var luck = halflingLuck.Modifiers;
			Assert.AreEqual(3, luck.Count);
		}

		[Fact]
		public void TraitsCanHaveConditionalModifiers() {
			var conditional = stoneCunning.Modifiers.OfType<ConditionalStatModifier>();
			Assert.AreEqual(1, conditional.Count());
			Assert.AreEqual("Stoneworking", conditional.First().Condition);
		}

		[Fact]
		public void TraitsCanHaveTags() {
			Assert.AreEqual("senses", darkvision.Tags.First());
		}

        [Fact]
        public void CanMarkSpecialAbilitiesAsWell()
        {
            Assert.Greater(darkvision.SpecialAbilities.Count, 0);
            Assert.AreEqual("Sight", darkvision.SpecialAbilities.First().Type);
            Assert.AreEqual("In Dark", darkvision.SpecialAbilities.First().Condition);
        }

        [Fact]
        public void TraitsCanHaveSpecialConfigurationSteps()
        {
            Assert.That(configureTrait.CustomConfiguration, Is.EqualTo("SilverNeedle.SomeOtherClass"));
        }

        [Fact]
        public void InitializingComponentWillCallCustomImplementation() 
        {
            var trait = new Trait();
            trait.CustomConfiguration = "Tests.Characters.CustomConfigurationSteps";
            var bag = new ComponentBag();
            trait.Initialize(bag);
            Assert.That(bag.GetAll<Feat>().Count(), Is.EqualTo(1));
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
- trait: 
  name: Configure
  configure: SilverNeedle.SomeOtherClass
...";
	}
        public class CustomConfigurationSteps : IComponent
        {
            public void Initialize(ComponentBag components)
            {
                //Add a feat or something
                components.Add(new Feat());
            }
        }
}
