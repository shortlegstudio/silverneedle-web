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

    public class DestructionTests : DomainTestBase<Destruction>
    {
        [SetUp]
        public void Configure()
        {
            base.InitializeDomain("destruction");
        }

        [Fact]
        public void DestructiveSmite()
        {
            var touch = character.Get<DestructiveSmite>();
            Assert.That(touch, Is.Not.Null); 
            Assert.That(touch.UsesPerDay, Is.EqualTo(6));
        }

        [Fact]
        public void DestructiveAura()
        {
            character.SetLevel(8);
            domain.LeveledUp(character.Components);
            var destAura = character.Get<DestructiveAura>();
            Assert.That(destAura, Is.Not.Null); 
        }
    }
}