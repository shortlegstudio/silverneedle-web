// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;

    public class LoreMasterTests
    {
        [Fact]
        public void LoreMasterCanBeUsedOncePerDayPlusEverySixLevelsAfter5()
        {
            var bard = CharacterTestTemplates.BardyBard();
            var loremaster = new LoreMaster();
            bard.SetLevel(5);
            bard.Add(loremaster);
            Assert.Equal(1, loremaster.UsesPerDay);
            bard.SetLevel(11);
            Assert.Equal(2, loremaster.UsesPerDay);
            bard.SetLevel(17);
            Assert.Equal(3, loremaster.UsesPerDay);
            Assert.Equal("Lore Master 3/day", loremaster.Name);
        }
    }
}