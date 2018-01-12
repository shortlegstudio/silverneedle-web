// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using Xunit;
    using SilverNeedle;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;

    public class LoreMasterTests
    {
        [Fact]
        public void LoreMasterHasSomeUsesPerDay()
        {
            var bard = CharacterTestTemplates.BardyBard();
            var loremaster = new LoreMaster();
            bard.Add(new BasicStat(loremaster.UsesPerDayStatName(), 1));
            bard.Add(loremaster);
            Assert.Equal(1, loremaster.UsesPerDay);
            Assert.Equal("Lore Master 1/day", loremaster.DisplayString());
        }
    }
}