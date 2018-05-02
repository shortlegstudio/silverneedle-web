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
        CharacterSheet bob30;
        CharacterSheet bob20;

        public MovementStatsTests()
        {
            bob30 = CharacterTestTemplates.AverageBob();
            move30 = bob30.Movement;

            bob20 = CharacterTestTemplates.AverageBob();
            move20 = bob20.Movement;
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
            bob30.Inventory.EquipItem(heavyArmor);
            Assert.Equal(move30.MovementSpeed, 20);
        }

        [Fact]
        public void ArmorMovementPenaltyIsAStatThatCanBeCalculated()
        {
            var armor = new Armor();
            armor.ArmorType = ArmorType.Heavy;
            bob30.Inventory.EquipItem(armor);
            bob20.Inventory.EquipItem(armor);
            var penalty = move30.ArmorMovementPenalty;
            Assert.Equal(penalty.TotalValue, -10);
            var penalty20 = move20.ArmorMovementPenalty;
            Assert.Equal(penalty20.TotalValue, -5);
        }
    }
}