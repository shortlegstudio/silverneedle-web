// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace Tests.Characters {
    using Xunit;
    using SilverNeedle.Characters;
    
    
    public class FeatTokenTests {
        [Fact]
        public void FeatTokensKnowWhetherAFeatMatches() {
            var combatFeat = new Feat();
            combatFeat.Tags.Add("combat");
            combatFeat.Tags.Add("critical");
            var magicFeat = new Feat();
            magicFeat.Tags.Add("item creation");

            var featToken = new FeatToken("combat");
            Assert.True(featToken.Qualifies(combatFeat));
            Assert.False(featToken.Qualifies(magicFeat));
        }

        [Fact]
        public void EmptyFeatTokenCanBeUsedForAnyFeat() {
            var f = new Feat();
            f.Tags.Add("combat");

            var featToken = new FeatToken();
            Assert.True(featToken.Qualifies(f));
            
        }

        [Fact]
        public void FeatTokensCanMatchByNameAsWell()
        {
            var feat = new Feat();
            feat.Name = "Turtle Snapper";
            var featToken = new FeatToken("Turtle Snapper");
            Assert.True(featToken.Qualifies(feat));
        }

        [Fact]
        public void CanReceiveAListOfFeats()
        {
            var featToken = new FeatToken(new string[] { "Dodge", "Mobility" });
            var dodge = Feat.Named("Dodge");
            var mobility = Feat.Named("Mobility");

            Assert.True(featToken.Qualifies(dodge));
            Assert.True(featToken.Qualifies(mobility));
        }
    }
}
