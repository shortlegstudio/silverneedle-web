// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Equipment
{
    using Xunit;
    using SilverNeedle.Equipment;
    
    public class MagicArmorTests : RequiresDataFiles
    {
        [Fact]
        public void MagicArmorAddsToTheArmorClassBonus()
        {
            var armor = new Armor();
            armor.Name = "Chainmail";
            armor.ArmorClass = 5;
            armor.ArmorCheckPenalty = -4;
            var magic = new MagicArmor(armor, 2);
            Assert.Equal(magic.ArmorClass, 7);
            Assert.Equal(magic.Value, 400000);
            Assert.Equal(magic.Name, "Chainmail +2");
            Assert.Equal(magic.ArmorCheckPenalty, -3);
        }
    }
}