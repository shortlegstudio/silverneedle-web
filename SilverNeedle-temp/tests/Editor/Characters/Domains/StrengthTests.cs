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

    public class StrengthTests : DomainTestBase<Strength>
    {
        [SetUp]
        public void Configure()
        {
            base.InitializeDomain("strength");
        }

        [Test]
        public void StrengthSurge()
        {
            var touch = character.Get<StrengthSurge>();
            Assert.That(touch, Is.Not.Null); 
            Assert.That(touch.UsesPerDay, Is.EqualTo(6));
        }

        [Test]
        public void MightOfGods()
        {
            character.SetLevel(8);
            domain.LeveledUp(character.Components);
            var aura = character.Get<MightOfTheGods>();
            Assert.That(aura, Is.Not.Null); 
        }
    }
}