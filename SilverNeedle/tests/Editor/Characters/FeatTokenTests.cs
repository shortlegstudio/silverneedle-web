using NUnit.Framework;
using SilverNeedle.Characters;

namespace Characters {
	[TestFixture]
	public class FeatTokenTests {
		[Test]
		public void FeatTokensKnowWhetherAFeatMatches() {
            var combatFeat = new Feat();
            combatFeat.Tags.Add("combat");
            combatFeat.Tags.Add("critical");
            var magicFeat = new Feat();
            magicFeat.Tags.Add("item creation");

            var featToken = new FeatToken("combat");
            Assert.IsTrue(featToken.Qualifies(combatFeat));
            Assert.IsFalse(featToken.Qualifies(magicFeat));
		}

        [Test]
        public void EmptyFeatTokenCanBeUsedForAnyFeat() {
            var f = new Feat();
            f.Tags.Add("combat");

            var featToken = new FeatToken();
            Assert.IsTrue(featToken.Qualifies(f));
            
        }
	}
}
