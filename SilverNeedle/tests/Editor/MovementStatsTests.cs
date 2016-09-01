using System;
using NUnit.Framework;
using SilverNeedle.Characters;

namespace Characters {

	[TestFixture]
	public class MovementStatsTests {
		[Test]
		public void CalculatesSquaresBasedOnMovementValue() {
			var move = new MovementStats(30);
			Assert.AreEqual(6, move.BaseSquares);
			move = new MovementStats(20);
			Assert.AreEqual(4, move.BaseSquares);
		}
	}
}

