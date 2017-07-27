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

    public class MadnessTests : DomainTestBase<Madness>
    {
        [SetUp]
        public void Configure()
        {
            base.InitializeDomain("madness");
        }

        [Fact]
        public void VisionOfMadness()
        {
            var touch = character.Get<VisionOfMadness>();
            Assert.That(touch, Is.Not.Null); 
            Assert.That(touch.UsesPerDay, Is.EqualTo(6));
        }

        [Fact]
        public void AuraOfMadness()
        {
            character.SetLevel(8);
            domain.LeveledUp(character.Components);
            var aura = character.Get<AuraOfMadness>();
            Assert.That(aura, Is.Not.Null); 
        }
    }
}