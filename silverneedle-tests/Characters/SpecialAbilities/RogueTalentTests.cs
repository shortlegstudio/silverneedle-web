// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using System.Linq;
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Utility;

    
    public class RogueTalentTests
    {
        [Fact]
        public void ParseAdvancedTalents()
        {
            var talentYaml = @"
- name: Talent
  advanced-talent: true".ParseYaml().Children.First();
            var talent = new RogueTalent(talentYaml);
            Assert.Equal(talent.Name, "Talent");
            Assert.True(talent.IsAdvancedTalent);
        }

        [Fact]
        public void ParseSneakAttackTalents()
        {
            var talentYaml = @"
- name: Talent
  sneak-attack: true".ParseYaml().Children.First();
            var talent = new RogueTalent(talentYaml);
            Assert.Equal(talent.Name, "Talent");
            Assert.True(talent.IsSneakAttack);
        }
    }
}