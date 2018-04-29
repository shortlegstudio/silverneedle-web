// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters
{
    using System.Linq;
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Serialization;

    public class SizeTests : RequiresDataFiles
    {
        [Fact]
        public void ReplacesOldSizeWhenAddedToCharacter()
        {
            var small = GatewayProvider.Find<Size>("small");
            var large = GatewayProvider.Find<Size>("large");

            var character = CharacterTestTemplates.AverageBob();
            character.Add(small);
            Assert.Equal(1, character.GetAll<Size>().Count());
            Assert.Equal(small, character.Get<Size>());

            character.Add(large);
            Assert.Equal(1, character.GetAll<Size>().Count());
            Assert.Equal(large, character.Get<Size>());
        }

        [Fact]
        public void UpdatesAssociatedStatisticsForTheValuesInTheSize()
        {
            var huge = GatewayProvider.Find<Size>("huge");

            var character = CharacterTestTemplates.AverageBob();
            character.Add(huge);
            AssertCharacter.StatValueIs(StatNames.SizeAttackModifier, huge.AttackModifier, character);
            AssertCharacter.StatValueIs(StatNames.SizeDefenseModifier, huge.DefenseModifier, character);
            AssertCharacter.StatValueIs(StatNames.SizeCombatManeuverModifier, huge.CombatManeuverModifier, character);
            AssertCharacter.StatValueIs(StatNames.SizeFlyModifier, huge.FlyModifier, character);
            AssertCharacter.StatValueIs(StatNames.SizeStealthModifier, huge.StealthModifier, character);
        }
    }
}