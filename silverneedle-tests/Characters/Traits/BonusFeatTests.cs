// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.Traits
{
    using Xunit;
    using System.Linq;
    using SilverNeedle;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Traits;

    public class BonusFeatTests
    {
        [Fact]
        public void AddsABonusFeatToken()
        {
            var bob = CharacterTestTemplates.AverageBob();
            var trait = new BonusFeat();
            bob.Add(trait);
            var bonus = bob.FeatTokens.First(x => x.Tags.Empty());
            Assert.NotNull(bonus);
        }
    }
}