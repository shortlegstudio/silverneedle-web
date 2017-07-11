// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Equipment
{
    using NUnit.Framework;
    using SilverNeedle.Equipment;
    
    [TestFixture]
    public class MagicArmorTests
    {
        [Test]
        public void MagicArmorAddsToTheArmorClassBonus()
        {
            var armor = new Armor();
            armor.Name = "Chainmail";
            armor.ArmorClass = 5;
            armor.ArmorCheckPenalty = -4;
            var magic = new MagicArmor(armor, 2);
            Assert.That(magic.ArmorClass, Is.EqualTo(7));
            Assert.That(magic.Value, Is.EqualTo(400000));
            Assert.That(magic.Name, Is.EqualTo("Chainmail +2"));
            Assert.That(magic.ArmorCheckPenalty, Is.EqualTo(-3));
        }
    }
}