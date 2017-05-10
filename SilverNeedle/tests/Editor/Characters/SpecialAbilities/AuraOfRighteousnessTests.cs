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
    public class AuraOfRighteousnessTests
    {
        [Test]
        public void EnablesDamageResistanceAgainstEvil()
        {
            var character = new CharacterSheet();
            character.Add(new AuraOfRighteousness());
            var defense = character.Get<DefenseStats>();
            var dr = defense.DamageResistance.First();
            Assert.That(dr.DamageType, Is.EqualTo("evil"));
            Assert.That(dr.Amount, Is.EqualTo(5));
        }

        [Test]
        public void ProvidesImmunityToCompulsion()
        {
            var character = new CharacterSheet();
            character.Add(new AuraOfRighteousness());
            var defense = character.Get<DefenseStats>();
            var righteous = defense.Immunities.First();
            Assert.That(righteous.Condition, Is.EqualTo("Compulsion"));
        }
    }
}