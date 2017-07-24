// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.Domains
{
    using System.Linq;
    using NUnit.Framework;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Domains;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Serialization;

    public class DarknessTests : DomainTestBase<Darkness>
    {
        [SetUp]
        public void Configure()
        {
            base.InitializeDomain("darkness");
        }

        [Test]
        public void TouchOfDarkness()
        {
            var touch = character.Get<TouchOfDarkness>();
            Assert.That(touch, Is.Not.Null); 
            Assert.That(touch.UsesPerDay, Is.EqualTo(6));
        }

        [Test]
        public void EyesOfDarkness()
        {
            character.SetLevel(8);
            domain.LeveledUp(character.Components);
            var eyeOfDark = character.Get<EyesOfDarkness>();
            Assert.That(eyeOfDark, Is.Not.Null); 
        }

        [Test]
        public void ReceiveBlindFightAsBonusFeat()
        {
            var blindFight = character.FeatTokens.First();
            Assert.That(blindFight.Tags, Is.EquivalentTo(new string[] { "Blind-Fight" })); 
        }
    }
}