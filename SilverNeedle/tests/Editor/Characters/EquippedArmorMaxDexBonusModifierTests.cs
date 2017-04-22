// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters
{
    using NUnit.Framework;
    using SilverNeedle.Utility;
    using SilverNeedle.Characters;

    [TestFixture]
    public class EquippedArmorMaxDexBonusModifierTests
    {
        [Test]
        public void IfNoEquippedArmorReturnTenThousand()
        {
            var bag = new ComponentBag();
            bag.Add(new Inventory());
            var mod = new EquippedArmorMaxDexBonuxModifier(bag);
            Assert.That(mod.Modifier, Is.EqualTo(10000));
        }
    } 
}