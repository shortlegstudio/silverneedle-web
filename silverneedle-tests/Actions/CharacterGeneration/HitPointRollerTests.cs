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

    
    public class HitPointRollerTests
    {
        [Fact]
        public void LevelUpRollerAddsSomeNumberOfHPBasedOnHitDiePlusCon() 
        {
            var character = new CharacterSheet();
            var cls = new Class();
            cls.HitDice = DiceSides.d8;
            character.AbilityScores.SetScore(AbilityScoreTypes.Constitution, 12);
            character.SetClass(cls);
            character.IncreaseHitPoints(9);
            
            var hpRoller = new HitPointRoller();
            hpRoller.ExecuteStep(character, new CharacterBuildStrategy());

            Assert.True(character.MaxHitPoints > 10);
            Assert.True(character.CurrentHitPoints > 10);

        }
    }
}