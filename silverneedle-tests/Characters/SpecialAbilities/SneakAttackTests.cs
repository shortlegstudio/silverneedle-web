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

    
    public class SneakAttackTests
    {
        [Fact]
        public void AddingSneakAttackConfiguresASpecialAttack()
        {
            var character = new CharacterSheet(CharacterStrategy.Default());
            character.InitializeComponents();
            var sneak = new SneakAttack();
            sneak.SetDamage("1d6");
            character.Add(sneak);
            var attack = character.Offense.Attacks().First(x => x.Name.Contains("Sneak Attack"));
            Assert.Equal(attack.Damage.ToString(), "1d6");

            // Update damage updates attack
            sneak.SetDamage("2d6");
            Assert.Equal(attack.Damage.ToString(), "2d6");
        }
    }
}