// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities.BloodlinePowers
{
    using Xunit;
    using SilverNeedle.Characters.SpecialAbilities.BloodlinePowers;

    public class ArcaneArcanaTests
    {
        [Fact]
        public void IncreaseDCForMetamagicFeats()
        {
            var arcana = new ArcaneArcana();
            Assert.Equal("+1 DC for metamagic spells that increase spell level", arcana.BonusAbility);
        }
    }
}