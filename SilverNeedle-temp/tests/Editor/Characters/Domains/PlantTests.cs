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

    public class PlantTests : DomainTestBase<Plant>
    {
        [SetUp]
        public void Configure()
        {
            base.InitializeDomain("plant");
        }

        [Test]
        public void WoodenFist()
        {
            var touch = character.Get<WoodenFist>();
            Assert.That(touch, Is.Not.Null); 
            Assert.That(touch.UsesPerDay, Is.EqualTo(6));
        }

        [Test]
        public void BrambleArmor()
        {
            character.SetLevel(6);
            domain.LeveledUp(character.Components);
            var armor = character.Get<BrambleArmor>();
            Assert.That(armor, Is.Not.Null); 
        }
    }
}