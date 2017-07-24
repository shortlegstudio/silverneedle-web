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

    public class ChaosTests : DomainTestBase<Chaos>
    {
        [SetUp]
        public void Configure()
        {
            base.InitializeDomain("chaos");
        }

        [Test]
        public void CanTouchOfChaos()
        {
            var touch = character.Get<TouchOfChaos>();
            Assert.That(touch, Is.Not.Null); 
            Assert.That(touch.UsesPerDay, Is.EqualTo(6));
        }

        [Test]
        public void CanMakeWeaponsChaotic()
        {
            character.SetLevel(8);
            domain.LeveledUp(character.Components);
            var chaosBlade = character.Get<ChaosBlade>();
            Assert.That(chaosBlade, Is.Not.Null); 
            Assert.That(chaosBlade.UsesPerDay, Is.EqualTo(1));
            character.SetLevel(16);

            Assert.That(chaosBlade.UsesPerDay, Is.EqualTo(3));
        }
    }
}