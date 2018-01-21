// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using System.Linq;
    using Xunit;
    using SilverNeedle;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Serialization;

    
    public class SneakAttackTests
    {
        [Fact]
        public void AddingSneakAttackConfiguresASpecialAttack()
        {
            var yaml = @"---
dice-stat:
  name: Sneak Attack Dice
  dice: 1d6";

            var sneakAttack = new SneakAttack(yaml.ParseYaml());
            var rogue = CharacterTestTemplates.Rogue();
            rogue.Add(sneakAttack);
            var attack = rogue.Offense.Attacks().First(x => x.Name.Contains("Sneak Attack"));
            Assert.Equal(attack.Damage.ToString(), "1d6");
        }

        [Fact]
        public void AddsSneakAttackDiceToComponents()
        {
            var yaml = @"---
dice-stat:
  name: Sneak Attack Dice
  dice: 1d6";

            var sneakAttack = new SneakAttack(yaml.ParseYaml());
            var rogue = CharacterTestTemplates.Rogue();
            rogue.Add(sneakAttack);
            Assert.NotNull(rogue.Components.FindStat("Sneak Attack Dice"));
            Assert.Equal("1d6", rogue.Components.FindStat<IDiceStatistic>("Sneak Attack Dice").Dice.ToString());
        }
    }
}