// Copyright (c) 2017 Trevor Redfern
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
    public class InitialHitPointsTests
    {
        [Test]
        public void MaxHitPointsAssignsTheCharacterHPToClassHDPlusConModifier() 
        {
            var character = new CharacterSheet();
            var cls = new Class();
            cls.HitDice = DiceSides.d8;
            character.SetClass(cls);
            character.AbilityScores.SetScore(AbilityScoreTypes.Constitution, 12);
            
            var hpRoller = new InitialHitPoints();
            hpRoller.Process(character, new CharacterBuildStrategy());

            Assert.AreEqual(9, character.MaxHitPoints);
            Assert.AreEqual(9, character.CurrentHitPoints);
        }
    }
}