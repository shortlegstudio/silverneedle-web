// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters {
	using NUnit.Framework;
	using SilverNeedle.Characters;

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