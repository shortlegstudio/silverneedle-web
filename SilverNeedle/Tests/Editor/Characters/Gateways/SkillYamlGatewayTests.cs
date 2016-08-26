using System;
using NUnit.Framework;
using System.Linq;
using SilverNeedle;
using SilverNeedle.Characters;
using SilverNeedle.Yaml;

namespace Characters {

	[TestFixture]
	public class SkillYamlGatewayTests {
		Skill Acrobatics;
		Skill Bluff;
		Skill DisableDevice;

		[SetUp]
		public void LoadYamlRepository() {
			var repo = new SkillYamlGateway (SkillsYamlFile.ParseYaml());
			var skills = repo.All();

			Acrobatics = skills.First (x => x.Name == "Acrobatics");
			Bluff = skills.First (x => x.Name == "Bluff");
			DisableDevice = skills.First (x => x.Name == "Disable Device");
		}

		[Test]
		public void LoadSkillsYamlFile() {
			Assert.IsNotNull (Acrobatics);
			Assert.IsNotNull (Bluff);
			Assert.IsNotNull (DisableDevice);
		}

		[Test]
		public void SkillsHaveAnAbilityToBaseScoresFrom() {
			Assert.AreEqual (AbilityScoreTypes.Dexterity, Acrobatics.Ability);
			Assert.AreEqual (AbilityScoreTypes.Charisma, Bluff.Ability);
			Assert.AreEqual (AbilityScoreTypes.Dexterity, DisableDevice.Ability);

		}

		[Test]
		public void SomeSkillsRequireTraining() {
			Assert.IsFalse (Acrobatics.TrainingRequired);
			Assert.IsTrue (DisableDevice.TrainingRequired);
		}

		[Test]
		public void SkillsCanHaveALongDescription() {
			Assert.AreEqual ("A really long description.\n", Acrobatics.Description);
		}

		private const string SkillsYamlFile = @"--- 
- skill: 
  name: Acrobatics
  ability: dexterity
  trained: no
  description: >
    A really long
    description.
- skill: 
  name: Bluff
  ability: charisma
  trained: no
  description: >
    A really long description.
- skill: 
  name: Disable Device
  ability: dexterity
  trained: yes
  description: >
    A really long description.
...";
	}
}


