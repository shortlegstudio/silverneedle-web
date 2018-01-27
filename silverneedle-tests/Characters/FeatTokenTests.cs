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
            var combatFeat = Feat.Named("Power Attack");
            combatFeat.Tags.Add("combat");
            combatFeat.Tags.Add("critical");
            var magicFeat = Feat.Named("Create Potion");
            magicFeat.Tags.Add("item creation");

            var featToken = new FeatToken("combat");
            Assert.True(featToken.Qualifies(combatFeat));
            Assert.False(featToken.Qualifies(magicFeat));
        }

        [Fact]
        public void EmptyFeatTokenCanBeUsedForAnyFeat() {
            var f = Feat.Named("Combat Reflexes");
            f.Tags.Add("combat");

            var featToken = new FeatToken();
            Assert.True(featToken.Qualifies(f));
            
        }

        [Fact]
        public void FeatTokensCanMatchByNameAsWell()
        {
            var feat = Feat.Named("Turtle Snapper");
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

        [Fact]
        public void IfFeatIsTaggedAsRequireSpecificTokenIgnoreEmptyToken()
        {
            var featToken = new FeatToken();
            var specialFeat = Feat.Named("Foo");
            specialFeat.Tags.Add("ignore-generic-token"); 
            Assert.False(featToken.Qualifies(specialFeat));
        }
    }
}
