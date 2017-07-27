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

    public class HealingTests : DomainTestBase<Healing>
    {
        [SetUp]
        public void Configure()
        {
            base.InitializeDomain("healing");
        }

        [Fact]
        public void RebukeDeath()
        {
            var touch = character.Get<RebukeDeath>();
            Assert.That(touch, Is.Not.Null); 
            Assert.That(touch.UsesPerDay, Is.EqualTo(6));
        }

        [Fact]
        public void HealerBlessing()
        {
            character.SetLevel(6);
            domain.LeveledUp(character.Components);
            var healBless = character.Get<HealerBlessing>();
            Assert.That(healBless, Is.Not.Null); 
        }
    }
}