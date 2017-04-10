// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters {
	using NUnit.Framework;
	using SilverNeedle.Characters;
	using SilverNeedle.Equipment;

	[TestFixture]
	public class MovementStatsTests {
		[Test]
		public void CalculatesSquaresBasedOnMovementValue() {
			var move = new MovementStats(30, new Inventory());
			Assert.AreEqual(6, move.BaseSquares);
			move = new MovementStats(20, new Inventory());
			Assert.AreEqual(4, move.BaseSquares);
		}

        [Test]
        public void ActualMoveSpeedCanBeImpactedByArmor()
        {
            var inventory = new Inventory();
            var heavyArmor = new Armor();
            heavyArmor.ArmorType = ArmorType.Heavy;
            inventory.EquipItem(heavyArmor);
            var move = new MovementStats(30,inventory);
            Assert.That(move.MovementSpeed, Is.EqualTo(20));
        }
    }
}