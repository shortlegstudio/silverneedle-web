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

    public class WeatherTests : DomainTestBase<Weather>
    {
        [SetUp]
        public void Configure()
        {
            base.InitializeDomain("weather");
        }

        [Test]
        public void StormBurst()
        {
            var touch = character.Get<StormBurst>();
            Assert.That(touch, Is.Not.Null); 
            Assert.That(touch.UsesPerDay, Is.EqualTo(6));
        }

        [Test]
        public void LightningLord()
        {
            character.SetLevel(8);
            domain.LeveledUp(character.Components);
            var aura = character.Get<LightningLord>();
            Assert.That(aura, Is.Not.Null); 
        }
    }
}