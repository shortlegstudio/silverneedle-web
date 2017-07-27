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

    public class ProtectionTests : DomainTestBase<Protection>
    {
        [SetUp]
        public void Configure()
        {
            base.InitializeDomain("protection");
        }

        [Fact]
        public void ResistantTouch()
        {
            var touch = character.Get<ResistantTouch>();
            Assert.That(touch, Is.Not.Null); 
            Assert.That(touch.UsesPerDay, Is.EqualTo(6));
        }

        [Fact]
        public void AuraOfProtection()
        {
            character.SetLevel(8);
            domain.LeveledUp(character.Components);
            var aura = character.Get<AuraOfProtection>();
            Assert.That(aura, Is.Not.Null); 
        }
    }
}