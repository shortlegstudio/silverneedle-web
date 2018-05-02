// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters
{
    using Xunit;
    using SilverNeedle.Utility;
    using SilverNeedle.Characters;

    
    public class EquippedArmorMaxDexBonusModifierTests
    {
        [Fact]
        public void IfNoEquippedArmorReturnTenThousand()
        {
            var bag = new ComponentContainer();
            bag.Add(new Inventory());
            var mod = new EquippedArmorMaxDexBonusModifier();
            bag.Add(mod);
            Assert.Equal(mod.Modifier, 10000);
        }
    } 
}