// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using System.Linq;
    using NUnit.Framework;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;

    [TestFixture]
    public class SneakAttackTests
    {
        [Test]
        public void AddingSneakAttackConfiguresASpecialAttack()
        {
            var character = new CharacterSheet();
            character.InitializeComponents();
            var sneak = new SneakAttack();
            sneak.SetDamage("1d6");
            character.Add(sneak);
            var attack = character.Offense.Attacks().First(x => x.Name.Contains("Sneak Attack"));
            Assert.That(attack.Damage.ToString(), Is.EqualTo("1d6"));

            // Update damage updates attack
            sneak.SetDamage("2d6");
            Assert.That(attack.Damage.ToString(), Is.EqualTo("2d6"));
        }
    }
}