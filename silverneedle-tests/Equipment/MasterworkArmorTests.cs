// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Equipment
{
    using Xunit;
    using SilverNeedle.Equipment;

    public class MasterworkArmorTests
    {
        [Fact]
        public void MasterworkArmorDecreasesArmorCheckPenaltyByOneAndIncreasesValue()
        {
            var armor = new Armor("Chainmail", 8, 58, 3, -5, 20, ArmorType.Medium);
            var mwkChainmail = new MasterworkArmor(armor);
            Assert.Equal(mwkChainmail.Name, "Masterwork Chainmail");
            Assert.Equal(mwkChainmail.ArmorCheckPenalty, -4);
            Assert.Equal(mwkChainmail.Value, 15000);
        }
    }
}