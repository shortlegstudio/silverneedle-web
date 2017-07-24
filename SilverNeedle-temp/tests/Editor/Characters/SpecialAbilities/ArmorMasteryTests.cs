// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using NUnit.Framework;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Utility;

    [TestFixture]
    public class ArmorMasteryTests
    {
        [Test]
        public void AddsDamageResistanceToDefenseStats()
        {
            var bag = new ComponentBag();
            var defstats = new DefenseStats();
            bag.Add(defstats);

            var am = new ArmorMastery(5, "-");
            am.Initialize(bag);
            Assert.That(defstats.DamageResistance, Contains.Item(am.DamageResistance));
        }
    }
}