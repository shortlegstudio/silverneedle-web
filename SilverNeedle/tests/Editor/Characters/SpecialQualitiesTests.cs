// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters 
{
    using System.Linq;
    using System.Collections.Generic;
    using NUnit.Framework;
    using SilverNeedle.Characters;

    [TestFixture]
    public class SpecialQualitiesTests
    {
        [Test]
        public void ProcessSpecialQualityAbilities()
        {
            var sq = new SpecialQualities();
            var abl = new SomeSpecialAbilities();
            sq.ProcessSpecialAbilities(abl);
            Assert.AreEqual(sq.SpecialAbilities.First(), abl.SpecialAbilities.First());
        }

        [Test]
        public void BreaksOutSightBasedAbilities()
        {
            var sq = new SpecialQualities();
            var abl = new SomeSpecialAbilities();
            sq.ProcessSpecialAbilities(abl);
            Assert.AreEqual(sq.SightAbilities.First(), abl.SpecialAbilities.Last());
        }

        [Test]
        public void CanAddSpecialAbilities()
        {
            var sq = new SpecialQualities();
            var ability = new SpecialAbility();
            sq.Add(ability);
            Assert.That(sq.SpecialAbilities.First(), Is.EqualTo(ability));
        }

        private class SomeSpecialAbilities : IProvidesSpecialAbilities
        {
            public System.Collections.Generic.IList<SpecialAbility> SpecialAbilities { get; set; }

            public SomeSpecialAbilities() 
            {
                SpecialAbilities = new List<SpecialAbility>();
                SpecialAbilities.Add(new SpecialAbility("Trapfinding +1", "Ability"));
                SpecialAbilities.Add(new SpecialAbility("Darkvision", "Sight"));
            }

        }
    }
}

