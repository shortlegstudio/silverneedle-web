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
    public class AuraOfCourageTests
    {
        [Test]
        public void AuraOfCourageEnablesImmunityToFear()
        {
            var character = new CharacterSheet();
            character.Add(new AuraOfCourage());
            var defense = character.Get<DefenseStats>();
            var fear = defense.Immunities.First();
            Assert.That(fear.Condition, Is.EqualTo("Fear"));
        }
    }
}