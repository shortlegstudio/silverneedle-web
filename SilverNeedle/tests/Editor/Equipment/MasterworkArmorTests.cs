// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Equipment
{
    using NUnit.Framework;
    using SilverNeedle.Equipment;

    [TestFixture]
    public class MasterworkArmorTests
    {
        [Test]
        public void MasterworkArmorDecreasesArmorCheckPenaltyByOneAndIncreasesValue()
        {
            var armor = new Armor("Chainmail", 8, 58, 3, -5, 20, ArmorType.Medium);
            var mwkChainmail = new MasterworkArmor(armor);
            Assert.That(mwkChainmail.Name, Is.EqualTo("Masterwork Chainmail"));
            Assert.That(mwkChainmail.ArmorCheckPenalty, Is.EqualTo(-4));
            Assert.That(mwkChainmail.Value, Is.EqualTo(15000));
        }
    }
}