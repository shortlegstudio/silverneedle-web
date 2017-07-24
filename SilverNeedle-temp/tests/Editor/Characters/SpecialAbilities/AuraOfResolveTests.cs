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
    public class AuraOfResolveTests
    {
        [Test]
        public void AuraOfResolveEnablesImmunityToCharms()
        {
            var character = new CharacterSheet();
            character.Add(new AuraOfResolve());
            var defense = character.Get<DefenseStats>();
            var resolve = defense.Immunities.First();
            Assert.That(resolve.Condition, Is.EqualTo("Charms"));
        }
    }
}