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

    public class LuckTests : DomainTestBase<Luck>
    {
        [SetUp]
        public void Configure()
        {
            base.InitializeDomain("luck");
        }

        [Test]
        public void BitOfLuck()
        {
            var touch = character.Get<BitOfLuck>();
            Assert.That(touch, Is.Not.Null); 
            Assert.That(touch.UsesPerDay, Is.EqualTo(6));
        }

        [Test]
        public void GoodFortune()
        {
            character.SetLevel(6);
            domain.LeveledUp(character.Components);
            var goodFortune = character.Get<GoodFortune>();
            Assert.That(goodFortune, Is.Not.Null); 
        }
    }
}