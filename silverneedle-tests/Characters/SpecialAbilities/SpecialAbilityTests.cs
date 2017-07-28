// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using Xunit;
    using SilverNeedle.Characters.SpecialAbilities;

    
    public class SpecialAbilityTests
    {
        [Fact]
        public void DefaultNameToTitlizedFromCamelCase()
        {
            var ability = new SpecialAbility();
            Assert.Equal(ability.Name, "Special Ability");

            var fancy = new SomeFancyAbility();
            Assert.Equal(fancy.Name, "Some Fancy Ability");
        }


        private class SomeFancyAbility : SpecialAbility
        {

        }
    }
}