// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using System.Linq;
    using Xunit;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    
    public class CombatStyleTests
    {
        [Fact]
        public void DeserializeSomeYamlToLoadTheAbility()
        {
            var yaml = @"
- name: Archery
  bonus-feats:
    - level: 1
      feats: feat one, feat two
    - level: 6
      feats: feat three, feat four".ParseYaml().Children.First();
            // TODO: The above is cumbersome for tests where I want to test 
            // just some basic configuration

            var combatStyle = new CombatStyle(yaml);
            Assert.Equal(combatStyle.Name, "Combat Style (Archery)");
            var level1feats = combatStyle.GetFeats(1);
            Assert.NotStrictEqual(level1feats, new string[] { "feat one", "feat two"});
            var level6feats = combatStyle.GetFeats(6);
            Assert.NotStrictEqual(level6feats, new string[] { "feat one", "feat two", "feat three", "feat four"});
        }
    }
}