// Copyright (c) 2016 Trevor Redfern
// 
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php

namespace Tests.Actions
{
    using NUnit.Framework;
    using SilverNeedle.Actions.CharacterGenerator;
    using SilverNeedle.Characters;
    using SilverNeedle.Dice;

    [TestFixture]
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
            hpRoller.Process(character, new CharacterBuildStrategy());

            Assert.Greater(character.MaxHitPoints, 10);
            Assert.Greater(character.CurrentHitPoints, 10);

        }
    }
}