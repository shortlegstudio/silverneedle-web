// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities.BloodlinePowers
{
    using Xunit;
    using SilverNeedle.Characters.SpecialAbilities.BloodlinePowers;

    public class AberrantArcanaTests
    {
        [Fact]
        public void IncreasePolymorphDurationBy50Percent()
        {
            var arcana = new AberrantArcana();
            Assert.Equal("+50% duration polymorph spells", arcana.BonusAbility);
            Assert.Equal("Aberrant Arcana (+50% duration polymorph spells)", arcana.DisplayString());
        }
    }
}