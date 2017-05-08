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
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Utility;

    [TestFixture]
    public class SpecialQualitiesTests
    {
        [Test]
        public void TracksAbilities()
        {
            var bag = new ComponentBag();
            var sq = new SpecialQualities();
            sq.Initialize(bag);
            bag.Add(new SpecialAbility());
            bag.Add(new SpecialAbility());
            Assert.That(sq.SpecialAbilities.Count(), Is.EqualTo(2));
        }

        [Test]
        public void BreaksOutSightBasedAbilities()
        {
            var bag = new ComponentBag();
            var sq = new SpecialQualities();
            sq.Initialize(bag);
            var darkVision = new SpecialAbility("Darkvision", "Sight");
            bag.Add(darkVision);
            Assert.That(sq.SightAbilities, Contains.Item(darkVision));
        }
    }
}