//-----------------------------------------------------------------------
// <copyright file="HitPointGenerator.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace SilverNeedle.Actions.CharacterGenerator
{
    using System.Collections.Generic;
    using SilverNeedle.Dice;
    using SilverNeedle.Characters;
    using System;

    /// <summary>
    /// Hit point generator rolls hitpoints for a character
    /// </summary>
    public class HitPointRoller : ICharacterBuildStep
    {
        /// <summary>
        /// Rolls the hit points.
        /// </summary>
        /// <returns>The hit points.</returns>
        /// <param name="character">Character to assign hitpoints to.</param>
        public int AddMaxHitPoints(CharacterSheet character)
        {
            // First Level is Max hit die + constitution bonus
            int hp = (int)character.Class.HitDice + character.AbilityScores.GetModifier(AbilityScoreTypes.Constitution);
            character.IncreaseHitPoints(hp);
            return hp;
        }

        /// <summary>
        /// Rolls the level up hit points
        /// </summary>
        /// <returns>The level up hitpoint amount.</returns>
        /// <param name="character">Character to roll hit points for.</param>
        public int AddLevelUpHitPoints(CharacterSheet character)
        {
            var cup = new Cup();
            cup.AddDie(new Die(character.Class.HitDice));
            cup.Modifier = character.AbilityScores.GetModifier(AbilityScoreTypes.Constitution);
            var roll = cup.Roll();
            character.IncreaseHitPoints(roll);
            return roll;
        }

        public void ProcessFirstLevel(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            AddMaxHitPoints(character);
        }

        public void ProcessLevelUp(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            AddLevelUpHitPoints(character);
        }
    }
}