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

    
    public class InitialHitPointsTests : RequiresDataFiles
    {
        [Fact]
        public void MaxHitPointsAssignsTheCharacterHPToClassHDPlusConModifier() 
        {
            var character = CharacterTestTemplates.AverageBob();
            var cls = Class.CreateForTesting("Druid", DiceSides.d8);
            character.SetClass(cls);
            character.AbilityScores.SetScore(AbilityScoreTypes.Constitution, 12);
            
            var hpRoller = new InitialHitPoints();
            hpRoller.ExecuteStep(character);

            Assert.Equal(9, character.HitPoints.TotalValue);
        }
    }
}