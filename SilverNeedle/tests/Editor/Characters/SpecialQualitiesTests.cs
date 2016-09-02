// //-----------------------------------------------------------------------
// // <copyright file="SpecialQualitiesTests.cs" company="Short Leg Studio, LLC">
// //     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------
using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;
using SilverNeedle;
using SilverNeedle.Characters;

namespace Characters 
{
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

