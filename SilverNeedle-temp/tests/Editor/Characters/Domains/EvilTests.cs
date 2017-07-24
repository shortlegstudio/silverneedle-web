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

    public class EvilTests : DomainTestBase<Evil>
    {
        [SetUp]
        public void Configure()
        {
            base.InitializeDomain("evil");
        }

        [Test]
        public void BleedingTouch()
        {
            var touch = character.Get<TouchOfEvil>();
            Assert.That(touch, Is.Not.Null); 
            Assert.That(touch.UsesPerDay, Is.EqualTo(6));
        }

        [Test]
        public void ScytheOfEvil()
        {
            character.SetLevel(8);
            domain.LeveledUp(character.Components);
            var evilScythe = character.Get<ScytheOfEvil>();
            Assert.That(evilScythe, Is.Not.Null); 
            Assert.That(evilScythe.UsesPerDay, Is.EqualTo(1));
            character.SetLevel(16);

            Assert.That(evilScythe.UsesPerDay, Is.EqualTo(3));}
    }
}