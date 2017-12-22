// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Utility;

    public class SpellLikeAbilityTests
    {
        [Fact]
        public void HasACertainNumberOfUsesPerDay()
        {
            var yaml = @"---
spell: ghost sound
per-day: 1";
            var spellLike = new SpellLikeAbility(yaml.ParseYaml());
            Assert.Equal(1, spellLike.UsesPerDay);
            Assert.Equal("ghost sound", spellLike.Spell);
            Assert.Equal("1/day - ghost sound", spellLike.DisplayString());
        }
    }
}