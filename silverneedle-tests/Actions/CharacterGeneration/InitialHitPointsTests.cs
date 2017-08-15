// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php

namespace Tests.Actions
{
    using Xunit;
    using SilverNeedle.Actions.CharacterGeneration;
    using SilverNeedle.Characters;
    using SilverNeedle.Dice;

    
    public class InitialHitPointsTests
    {
        [Fact]
        public void MaxHitPointsAssignsTheCharacterHPToClassHDPlusConModifier() 
        {
            var character = new CharacterSheet();
            var cls = new Class();
            cls.HitDice = DiceSides.d8;
            character.SetClass(cls);
            character.AbilityScores.SetScore(AbilityScoreTypes.Constitution, 12);
            
            var hpRoller = new InitialHitPoints();
            hpRoller.ExecuteStep(character, new CharacterBuildStrategy());

            Assert.Equal(9, character.MaxHitPoints);
            Assert.Equal(9, character.CurrentHitPoints);
        }
    }
}