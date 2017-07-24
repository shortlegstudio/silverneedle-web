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

    public class MagicTests : DomainTestBase<Magic>
    {
        [SetUp]
        public void Configure()
        {
            base.InitializeDomain("magic");
        }

        [Test]
        public void HandOfTheAcolyte()
        {
            var touch = character.Get<HandOfTheAcolyte>();
            Assert.That(touch, Is.Not.Null); 
            Assert.That(touch.UsesPerDay, Is.EqualTo(6));
        }

        [Test]
        public void DispellingTouch()
        {
            character.SetLevel(8);
            domain.LeveledUp(character.Components);
            var dispel = character.Get<DispellingTouch>();
            Assert.That(dispel, Is.Not.Null); 
            Assert.That(dispel.UsesPerDay, Is.EqualTo(1));
            character.SetLevel(16);

            Assert.That(dispel.UsesPerDay, Is.EqualTo(3));
        }
    }
}