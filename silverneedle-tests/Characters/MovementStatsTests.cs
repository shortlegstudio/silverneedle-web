// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters 
{
    using System.Linq;
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Equipment;
    using SilverNeedle.Utility;

    
    public class MovementStatsTests {
        MovementStats move30;
        MovementStats move20;
        Inventory inventory;

        public MovementStatsTests()
        {
            var bag30 = new ComponentBag();
            inventory = new Inventory();
            bag30.Add(inventory);
            move30 = new MovementStats();
            bag30.Add(move30);
            move30.SetBaseSpeed(30);

            var bag20 = new ComponentBag();
            bag20.Add(inventory);
            move20 = new MovementStats();
            bag20.Add(move20);
            move20.SetBaseSpeed(20);
        }

        [Fact]
        public void CalculatesSquaresBasedOnMovementValue() {
            Assert.Equal(6, move30.BaseSquares);
            Assert.Equal(4, move20.BaseSquares);
        }

        [Fact]
        public void ActualMoveSpeedCanBeImpactedByArmor()
        {
            var heavyArmor = new Armor();
            heavyArmor.ArmorType = ArmorType.Heavy;
            inventory.EquipItem(heavyArmor);
            Assert.Equal(move30.MovementSpeed, 20);
        }

        [Fact]
        public void ExposeMovementStatistics()
        {
            var stats = move30.Statistics.Select(x => x.Name);
            Assert.NotStrictEqual(stats, new string[] { StatNames.BaseMovementSpeed, StatNames.ArmorMovementPenalty });
        }

        [Fact]
        public void ArmorMovementPenaltyIsAStatThatCanBeCalculated()
        {
            var armor = new Armor();
            armor.ArmorType = ArmorType.Heavy;
            inventory.EquipItem(armor);
            var penalty = move30.ArmorMovementPenalty;
            Assert.Equal(penalty.TotalValue, -10);
            var penalty20 = move20.ArmorMovementPenalty;
            Assert.Equal(penalty20.TotalValue, -5);
        }
    }
}