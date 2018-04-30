// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;

    
    public class LayOnHandsTests : RequiresDataFiles
    {
        [Fact]
        public void CreatesAStatForTrackingUsesPerDay()
        {
            var character = CharacterTestTemplates.AverageBob();
            var lay = new LayOnHands();
            character.Add(lay);
            Assert.NotNull(character.FindStat(lay.UsesPerDayStatName()));
        }

        [Fact]
        public void AddsDiceStatisticForHealingDice()
        {
            var character = CharacterTestTemplates.AverageBob();
            var lay = new LayOnHands();
            character.Add(lay);
            Assert.NotNull(character.FindStat("Lay On Hands Dice"));
        }
    }
}