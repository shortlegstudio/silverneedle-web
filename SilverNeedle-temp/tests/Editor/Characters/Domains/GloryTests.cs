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

    public class GloryTests : DomainTestBase<Glory>
    {
        [SetUp]
        public void Configure()
        {
            base.InitializeDomain("glory");
        }

        [Test]
        public void TouchOfGlory()
        {
            var touch = character.Get<TouchOfGlory>();
            Assert.That(touch, Is.Not.Null); 
            Assert.That(touch.UsesPerDay, Is.EqualTo(6));
        }

        [Test]
        public void DivinePresence()
        {
            character.SetLevel(8);
            domain.LeveledUp(character.Components);
            var divinePres = character.Get<DivinePresence>();
            Assert.That(divinePres, Is.Not.Null); 
        }
    }
}