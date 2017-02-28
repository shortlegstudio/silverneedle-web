using NUnit.Framework;
using System.Linq;
using SilverNeedle.Characters;
using SilverNeedle.Utility;

namespace Characters {
	[TestFixture]
	public class SkillGatewayTests {

		[Test]
		public void EnsureWeHaveASkillsFile() {
			var repo = new EntityGateway<Skill>();
			var skills = repo.All();
			Assert.Greater(skills.Count(), 0);
		}
	}
}


