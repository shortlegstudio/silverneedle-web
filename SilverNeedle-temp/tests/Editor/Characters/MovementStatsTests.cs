// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters 
{
    using System.Linq;
    using NUnit.Framework;
    using SilverNeedle.Characters;
    using SilverNeedle.Equipment;
    using SilverNeedle.Utility;

    [TestFixture]
    public class MovementStatsTests {
        MovementStats move30;
        MovementStats move20;
        Inventory inventory;

        [SetUp]
        public void Configure()
        {
            var bag30 = new ComponentBag();
            inventory = new Inventory();
            bag30.Add(inventory);
            move30 = new MovementStats();
            bag30.Add(move30);
            move30.Initialize(bag30);
            move30.SetBaseSpeed(30);

            var bag20 = new ComponentBag();
            bag20.Add(inventory);
            move20 = new MovementStats();
            bag20.Add(move20);
            move20.Initialize(bag20);
            move20.SetBaseSpeed(20);
        }

        [Fact]
        public void CalculatesSquaresBasedOnMovementValue() {
            Assert.AreEqual(6, move30.BaseSquares);
            Assert.AreEqual(4, move20.BaseSquares);
        }

        [Fact]
        public void ActualMoveSpeedCanBeImpactedByArmor()
        {
            var heavyArmor = new Armor();
            heavyArmor.ArmorType = ArmorType.Heavy;
            inventory.EquipItem(heavyArmor);
            Assert.That(move30.MovementSpeed, Is.EqualTo(20));
        }

        [Fact]
        public void ExposeMovementStatistics()
        {
            var stats = move30.Statistics.Select(x => x.Name);
            Assert.That(stats, Is.EquivalentTo(new string[] { StatNames.BaseMovementSpeed, StatNames.ArmorMovementPenalty }));
        }

        [Fact]
        public void ArmorMovementPenaltyIsAStatThatCanBeCalculated()
        {
            var armor = new Armor();
            armor.ArmorType = ArmorType.Heavy;
            inventory.EquipItem(armor);
            var penalty = move30.ArmorMovementPenalty;
            Assert.That(penalty.TotalValue, Is.EqualTo(-10));
            var penalty20 = move20.ArmorMovementPenalty;
            Assert.That(penalty20.TotalValue, Is.EqualTo(-5));
        }
    }
}