using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;
using SilverNeedle;
using SilverNeedle.Characters;
using SilverNeedle.Yaml;

namespace Characters {

	[TestFixture]
	public class FeatTests {
		Feat Acrobatic;
		Feat CombatExpertise;
		Feat PowerAttack;
		Feat CraftWand;

		[SetUp]
		public void SetUp() {
			var feats = Feat.LoadFromYaml (FeatYamlFile.ParseYaml());
			Acrobatic = feats.First (x => x.Name == "Acrobatic");
			CombatExpertise = feats.First (x => x.Name == "Combat Expertise");
			PowerAttack = feats.First (x => x.Name == "Power Attack");
			CraftWand = feats.First (x => x.Name == "Craft Wand");
		}
			
	    [Test]
	    public void LoadTraitYamlFile() {
			Assert.IsNotNull (Acrobatic);
			Assert.IsNotNull (CombatExpertise);
			Assert.IsNotNull (PowerAttack);
	    }

		[Test]
		public void FeatsHaveADescription() {
			Assert.AreEqual ("Move good", Acrobatic.Description);
			Assert.AreEqual ("Hit Stuff Hard", PowerAttack.Description);
		}

		[Test]
		public void FeatsCanHaveSkillAdjustments() {
			var modifiers = Acrobatic.Modifiers;
			Assert.AreEqual (2, modifiers.Count);
			var skillAdj = modifiers.First ();
			Assert.AreEqual ("Acrobatics", skillAdj.StatisticName);
			Assert.AreEqual("bonus", skillAdj.Type);
			Assert.AreEqual ("Acrobatic (feat)", skillAdj.Reason);
			Assert.AreEqual (2, skillAdj.Modifier);

			var flyAdj = modifiers.Last ();
			Assert.AreEqual ("Fly", flyAdj.StatisticName);
			Assert.AreEqual("bonus", skillAdj.Type);
			Assert.AreEqual ("Acrobatic (feat)", skillAdj.Reason);
			Assert.AreEqual (4, flyAdj.Modifier);
		}

		[Test]
		public void FeatsCanHaveAbilityPrerequisites() {
			var prereq = CombatExpertise.Prerequisites;
			var abilityCheck = prereq.First () as Prerequisites.AbilityPrerequisite;
            Assert.IsInstanceOf<Prerequisites.AbilityPrerequisite> (abilityCheck);
			Assert.AreEqual (AbilityScoreTypes.Intelligence, abilityCheck.Ability);
			Assert.AreEqual (13, abilityCheck.Minimum);
		}

		[Test]
		public void SomeFeatsAreCombatFeats() {
			Assert.IsTrue (CombatExpertise.IsCombatFeat);
			Assert.IsFalse (Acrobatic.IsCombatFeat);
		}

		[Test]
		public void SomeFeatsAreCriticalFeats() {
			Assert.IsTrue (PowerAttack.IsCriticalFeat);
			Assert.IsFalse (Acrobatic.IsCriticalFeat);
		}

		[Test]
		public void SomeFeatsAreItemCreationFeats() {
			Assert.IsTrue (CraftWand.IsItemCreation);
			Assert.IsFalse (Acrobatic.IsItemCreation);
		}

		[Test]
		public void FeatsKnowWhetherYouQualify() {
			var smartCharacter = new CharacterSheet (new List<Skill>());
			smartCharacter.AbilityScores.SetScore (AbilityScoreTypes.Intelligence, 15);
			var dumbCharacter = new CharacterSheet (new List<Skill>());
			dumbCharacter.AbilityScores.SetScore (AbilityScoreTypes.Intelligence, 5);
			Assert.IsTrue (CombatExpertise.IsQualified (smartCharacter));
			Assert.IsFalse (CombatExpertise.IsQualified (dumbCharacter));
		}

		[Test]
		public void FeatsCanBeFilteredByGroup() {

		}

		private const string FeatYamlFile = @"--- 
- feat: 
  name: Acrobatic
  description: Move good
  modifiers:
    - type: bonus
      stat: Acrobatics
      modifier: 2
    - type: bonus
      stat: Fly
      modifier: 4
- feat:
  name: Combat Expertise
  description: Dodge stuff better
  tags: combat
  prerequisites:
    - ability: Intelligence 13
- feat:
  name: Power Attack
  description: Hit Stuff Hard
  tags: combat, critical
- feat:
  name: Craft Wand
  tags: itemcreation
  description: Make Wands
...";
	}
}
