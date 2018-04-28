// Copyright (c) 2016 Trevor Redfern
// 
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php

namespace Tests.Actions
{
    using Xunit;
    using SilverNeedle.Actions.CharacterGeneration;
    using SilverNeedle.Characters;
    using SilverNeedle.Dice;
    using SilverNeedle;

    public class HitPointRollerTests : RequiresDataFiles
    {
        [Fact]
        public void LevelUpRollerAddsSomeNumberOfHPBasedOnHitDiePlusCon() 
        {
            var character = CharacterTestTemplates.AverageBob();
            var cls = Class.CreateForTesting("Cleric", DiceSides.d8);
            character.AbilityScores.SetScore(AbilityScoreTypes.Constitution, 12);
            character.SetClass(cls);
            
            var hpRoller = new HitPointRoller();
            hpRoller.ExecuteStep(character);

            Assert.True(character.HitPoints.TotalValue >= 2);
        }

    }
}