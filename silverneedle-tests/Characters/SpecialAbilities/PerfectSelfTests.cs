// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using Xunit;
    using System.Linq;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;

    public class PerfectSelfTests
    {
        [Fact]
        public void AddsDamageResistance()
        {
            var monk = CharacterTestTemplates.MarkyMonk();
            monk.Add(new PerfectSelf());
            var defense = monk.Get<DefenseStats>();
            var dr = defense.EnergyResistance.First();
            Assert.Equal(10, dr.Amount);
            Assert.Equal("chaotic", dr.DamageType);
        }
    }
}