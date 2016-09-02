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
	}

}
