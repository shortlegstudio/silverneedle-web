// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Utility;

    
    public class ArmorMasteryTests
    {
        [Fact]
        public void AddsDamageResistanceToDefenseStats()
        {
            var character = CharacterTestTemplates.AverageBob();
            var defstats = character.Get<DefenseStats>();

            var am = new ArmorMastery(5, "-");
            character.Add(am);
            Assert.Contains(am.DamageResistance, defstats.EnergyResistance);
        }
    }
}