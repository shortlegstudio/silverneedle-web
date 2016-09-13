// Copyright (c) 2016 Trevor Redfern
// 
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php


namespace Characters {
		using NUnit.Framework;
		using SilverNeedle.Characters;

		[TestFixture]
		public class SkillTests {
				[Test]
				public void CanDetectCraftingSkills()
				{
						var skill = new Skill("Craft (Pottery)", AbilityScoreTypes.Intelligence, false);
						Assert.IsTrue(skill.IsCraftSkill);
				}


		}
}