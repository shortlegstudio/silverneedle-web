// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters 
{
    using System.Linq;
    using System.Collections.Generic;
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Utility;

    
    public class SpecialQualitiesTests
    {
        [Fact]
        public void TracksAbilities()
        {
            var bag = new ComponentBag();
            var sq = new SpecialQualities();
            sq.Initialize(bag);
            bag.Add(new SpecialAbility());
            bag.Add(new SpecialAbility());
            Assert.Equal(sq.SpecialAbilities.Count(), 2);
        }
    }
}