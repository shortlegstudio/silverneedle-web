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
        [Test]
        public void MaxHitPointsAssignsTheCharacterHPToClassHDPlusConModifier() 
        {
            var character = new CharacterSheet();
            var cls = new Class();
            cls.HitDice = DiceSides.d8;
            character.SetClass(cls);
            character.AbilityScores.SetScore(AbilityScoreTypes.Constitution, 12);
            
            var hpRoller = new HitPointRoller();
            hpRoller.AddMaxHitPoints(character);

            Assert.AreEqual(9, character.MaxHitPoints);
            Assert.AreEqual(9, character.CurrentHitPoints);
        }

        [Test]
        public void LevelUpRollerAddsSomeNumberOfHPBasedOnHitDiePlusCon() 
        {
            var character = new CharacterSheet();
            var cls = new Class();
            cls.HitDice = DiceSides.d8;
            character.AbilityScores.SetScore(AbilityScoreTypes.Constitution, 12);
            character.SetClass(cls);

            var hpRoller = new HitPointRoller();
            hpRoller.AddMaxHitPoints(character);
            hpRoller.AddLevelUpHitPoints(character);

            Assert.Greater(character.MaxHitPoints, 10);
            Assert.Greater(character.CurrentHitPoints, 10);

        }
    }
}