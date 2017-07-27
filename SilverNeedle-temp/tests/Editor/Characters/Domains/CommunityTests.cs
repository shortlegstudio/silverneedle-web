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

    public class CommunityTests : DomainTestBase<Community>
    {
        [SetUp]
        public void Configure()
        {
            base.InitializeDomain("community");
        }

        [Fact]
        public void CanCalmingTouch()
        {
            var touch = character.Get<CalmingTouch>();
            Assert.That(touch, Is.Not.Null); 
            Assert.That(touch.UsesPerDay, Is.EqualTo(6));
        }

        [Fact]
        public void CanGiveUnity()
        {
            character.SetLevel(8);
            domain.LeveledUp(character.Components);
            var unity = character.Get<Unity>();
            Assert.That(unity, Is.Not.Null); 
            
            Assert.That(unity.UsesPerDay, Is.EqualTo(1));
            character.SetLevel(16);

            Assert.That(unity.UsesPerDay, Is.EqualTo(3));
        }
    }
}