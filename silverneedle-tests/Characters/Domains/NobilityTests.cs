// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.Domains
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Domains;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Serialization;

    public class NobilityTests : DomainTestBase<Nobility>
    {
        public NobilityTests()
        {
            base.InitializeDomain("nobility");
        }

        [Fact]
        public void InspiringWord()
        {
            var touch = character.Get<InspiringWord>();
            Assert.NotNull(touch);
            Assert.Equal(touch.UsesPerDay, 6);
        }

        [Fact]
        public void Leadership()
        {
            character.SetLevel(8);
            domain.LeveledUp(character.Components);
            var featToken = character.FeatTokens[0];
            Assert.NotStrictEqual(featToken.Tags, new string[] { "Leadership" });
        }
    }
}