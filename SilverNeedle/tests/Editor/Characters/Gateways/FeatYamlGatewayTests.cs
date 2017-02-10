// Copyright (c) 2016 Trevor Redfern
// 
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php

namespace Characters
{
    using System.Linq;
    using NUnit.Framework;
    using SilverNeedle.Characters;
    using SilverNeedle.Yaml;

    [TestFixture]
    public class FeatYamlGatewayTests {
        Feat Acrobatic;
        Feat CombatExpertise;
        Feat PowerAttack;
        Feat CraftWand;

        IFeatGateway featGateway;
        [SetUp]
        public void SetUp() {
            this.featGateway = new FeatGateway(FeatYamlFile.ParseYaml());
            Acrobatic = featGateway.GetByName("Acrobatic");
            CombatExpertise = featGateway.GetByName("Combat Expertise");
            PowerAttack = featGateway.GetByName("Power Attack");
            CraftWand = featGateway.GetByName("Craft Wand");
        }

        [Test]
        public void CanFetchFeatsByName()
        {
            var acro = featGateway.GetByName("Acrobatic");
            Assert.AreEqual("Acrobatic", acro.Name);
        }

        [Test]
        public void CanFetchFeatsByNameIsCaseInsensitive()
        {
            var acro = featGateway.GetByName("acrobatic");
            Assert.AreEqual("Acrobatic", acro.Name);
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

        public void CanReturnQualifyingFeats() {
            var character = new CharacterSheet();
            character.AbilityScores.SetScore(AbilityScoreTypes.Intelligence, 13);
            var qual = featGateway.GetQualifyingFeats(character);
            Assert.AreEqual(1, qual.Count());
            Assert.AreEqual(CombatExpertise, qual.First());

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