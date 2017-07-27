// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.Domains
{
    using NUnit.Framework;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Domains;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Serialization;

    public class NobilityTests : DomainTestBase<Nobility>
    {
        [SetUp]
        public void Configure()
        {
            base.InitializeDomain("nobility");
        }

        [Fact]
        public void InspiringWord()
        {
            var touch = character.Get<InspiringWord>();
            Assert.That(touch, Is.Not.Null); 
            Assert.That(touch.UsesPerDay, Is.EqualTo(6));
        }

        [Fact]
        public void Leadership()
        {
            character.SetLevel(8);
            domain.LeveledUp(character.Components);
            var featToken = character.FeatTokens[0];
            Assert.That(featToken.Tags, Is.EquivalentTo(new string[] { "Leadership" }));
        }
    }
}