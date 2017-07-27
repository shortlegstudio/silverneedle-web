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

    public class LiberationTests : DomainTestBase<Liberation>
    {
        [SetUp]
        public void Configure()
        {
            base.InitializeDomain("liberation");
        }

        [Fact]
        public void Liberation()
        {
            var touch = character.Get<LiberationMobility>();
            Assert.That(touch, Is.Not.Null); 
        }

        [Fact]
        public void FreedomCall()
        {
            character.SetLevel(8);
            domain.LeveledUp(character.Components);
            var free = character.Get<FreedomCall>();
            Assert.That(free, Is.Not.Null); 
        }
    }
}