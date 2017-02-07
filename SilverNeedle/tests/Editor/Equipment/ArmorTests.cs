using System;
using NUnit.Framework;
using SilverNeedle.Equipment;


namespace Equipment {
	[TestFixture]
	public class ArmorTests {
		[Test]
		public void DefaultArmorTypeIsNone() {
			var armor = new Armor ();
			Assert.AreEqual (ArmorType.None, armor.ArmorType);
		}

        [Test]
        public void ReductionInSpeedForNoArmorIsZero()
        {
            var armor = new Armor();
            armor.ArmorType = ArmorType.None;
            Assert.AreEqual(0, armor.MovementSpeedPenalty);            
        }

        [Test]
        public void ReductionInSpeedForLightArmorIsZero()
        {
            var armor = new Armor();
            armor.ArmorType = ArmorType.Light;
            Assert.AreEqual(0, armor.MovementSpeedPenalty);
        }

        [Test]
        public void ReductionInSpeedForMediumArmorIsTen()
        {
            var armor = new Armor();
            armor.ArmorType = ArmorType.Medium;
            Assert.AreEqual(10, armor.MovementSpeedPenalty);            
        }

        [Test]
        public void ReductionInSpeedForHeavyArmorIsTen()
        {
            var armor = new Armor();
            armor.ArmorType = ArmorType.Heavy;
            Assert.AreEqual(10, armor.MovementSpeedPenalty);            
        }

	}
}
