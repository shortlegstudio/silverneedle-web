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

    public class CharmTests : DomainTestBase<Charm>
    {
        [SetUp]
        public void Configure()
        {
            base.InitializeDomain("charm");
        }

        [Test]
        public void CanDazeCreations()
        {
            var touch = character.Get<DazingTouch>();
            Assert.That(touch, Is.Not.Null); 
            Assert.That(touch.UsesPerDay, Is.EqualTo(6));
        }

        [Test]
        public void CharmingSmile()
        {
            character.SetLevel(8);
            domain.LeveledUp(character.Components);
            var charmingSmile = character.Get<CharmingSmile>();
            Assert.That(charmingSmile, Is.Not.Null); 
        }
    }
}