// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using Xunit;
    using SilverNeedle.Characters.SpecialAbilities;

    public class ForceMissileTests
    {
        [Fact]
        public void Does1d4PlusIntenseSpellsDamage()
        {
            var wizard = CharacterTestTemplates.Wizard();
            var intense = new IntenseSpells();
            var force = new ForceMissile();
            wizard.Add(intense);
            wizard.Add(force);

            Assert.Equal("1d4+1", force.Damage.ToString());
            Assert.Equal(3, force.UsesPerDay);
            
            Assert.Equal("Force Missile 1d4+1 (3/day)", force.DisplayString());
        }
    }
}