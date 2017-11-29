// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities.BloodlinePowers
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities.BloodlinePowers;

    public class ElementalMovementTests
    {
        [Fact]
        public void NameContainsElementalMovementStats()
        {
            var sorcerer = CharacterTestTemplates.Sorcerer();
            var type = new ElementalType();
            type.MovementSpeed = 30;
            type.MovementType = MovementType.Fly;

            var move = new ElementalMovement();
            sorcerer.Add(type);
            sorcerer.Add(move);

            Assert.Contains("30' Fly", move.Name);
        }
    }
}