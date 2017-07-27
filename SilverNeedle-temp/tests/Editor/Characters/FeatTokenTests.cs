using NUnit.Framework;
using SilverNeedle.Characters;

namespace Characters {
	[TestFixture]
	public class FeatTokenTests {
		[Fact]
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

        [Fact]
        public void EmptyFeatTokenCanBeUsedForAnyFeat() {
            var f = new Feat();
            f.Tags.Add("combat");

            var featToken = new FeatToken();
            Assert.IsTrue(featToken.Qualifies(f));
            
        }

        [Fact]
        public void FeatTokensCanMatchByNameAsWell()
        {
            var feat = new Feat();
            feat.Name = "Turtle Snapper";
            var featToken = new FeatToken("Turtle Snapper");
            Assert.IsTrue(featToken.Qualifies(feat));
        }
	}
}
