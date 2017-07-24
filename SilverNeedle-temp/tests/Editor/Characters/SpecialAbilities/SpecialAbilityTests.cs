// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using NUnit.Framework;
    using SilverNeedle.Characters.SpecialAbilities;

    [TestFixture]
    public class SpecialAbilityTests
    {
        [Test]
        public void DefaultNameToTitlizedFromCamelCase()
        {
            var ability = new SpecialAbility();
            Assert.That(ability.Name, Is.EqualTo("Special Ability"));

            var fancy = new SomeFancyAbility();
            Assert.That(fancy.Name, Is.EqualTo("Some Fancy Ability"));
        }


        private class SomeFancyAbility : SpecialAbility
        {

        }
    }
}