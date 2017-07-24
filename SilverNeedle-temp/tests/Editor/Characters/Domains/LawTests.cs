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

    public class LawTests : DomainTestBase<Law>
    {
        [SetUp]
        public void Configure()
        {
            base.InitializeDomain("law");
        }

        [Test]
        public void CanTouchOfLaw()
        {
            var touch = character.Get<TouchOfLaw>();
            Assert.That(touch, Is.Not.Null); 
            Assert.That(touch.UsesPerDay, Is.EqualTo(6));
        }

        [Test]
        public void CanMakeWeaponsLawful()
        {
            character.SetLevel(8);
            domain.LeveledUp(character.Components);
            var staffOrder = character.Get<StaffOfOrder>();
            Assert.That(staffOrder, Is.Not.Null); 
            Assert.That(staffOrder.UsesPerDay, Is.EqualTo(1));
            character.SetLevel(16);

            Assert.That(staffOrder.UsesPerDay, Is.EqualTo(3));
        }
    }
}