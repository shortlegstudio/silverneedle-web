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

    public class SunTests : DomainTestBase<Sun>
    {
        [SetUp]
        public void Configure()
        {
            base.InitializeDomain("sun");
        }

        [Fact]
        public void SunBlessing()
        {
            var touch = character.Get<SunBlessing>();
            Assert.That(touch, Is.Not.Null); 
        }

        [Fact]
        public void NimbusOfLight()
        {
            character.SetLevel(8);
            domain.LeveledUp(character.Components);
            var aura = character.Get<NimbusOfLight>();
            Assert.That(aura, Is.Not.Null); 
        }
    }
}