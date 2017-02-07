﻿//-----------------------------------------------------------------------
// <copyright file="LevelUpGenerator.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace SilverNeedle.Actions.CharacterGenerator
{
    using SilverNeedle;
    using SilverNeedle.Characters;

    /// <summary>
    /// Level up generator handles adding more levels to a character in a specific class
    /// </summary>
    public class LevelUpCharacter
    {
        /// <summary>
        /// The HP generator rolls the hit points
        /// </summary>
        private HitPointRoller hitPointGenerator;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="SilverNeedle.Actions.CharacterGenerator.LevelUpCharacter"/> class.
        /// </summary>
        /// <param name="hitPointGen">Generator for hit points.</param>
        public LevelUpCharacter(HitPointRoller hitPointGen)
        {
            this.hitPointGenerator = hitPointGen;
        }

        /// <summary>
        /// Brings the character to level.
        /// </summary>
        /// <param name="character">Character to level up.</param>
        /// <param name="targetLevel">Target level.</param>
        public void BringCharacterToLevel(CharacterSheet character, int targetLevel)
        {
            for (int i = character.Level; i <= targetLevel; i++)
            {
                this.LevelUp(character);
            }
        }

        /// <summary>
        /// Levels up a single level
        /// </summary>
        /// <param name="character">Character to level up.</param>
        public void LevelUp(CharacterSheet character)
        {
            character.SetLevel(character.Level + 1);
            this.hitPointGenerator.AddLevelUpHitPoints(character);            
            character.Defense.LevelUpDefenseStats(character.Class);
            AddSpecialAbilities(character, character.Class.GetLevel(character.Level));

            if(character.Level % 2 == 1) {
                character.FeatTokens.Add(new FeatToken());
            }
            
            // Special Level ups
            if (character.Level % 4 == 0)
            {
                this.AssignAbilityPoints(character);
            }
        }

        /// <summary>
        /// Assigns the ability points.
        /// </summary>
        /// <param name="character">Character to assign an ability point to.</param>
        private void AssignAbilityPoints(CharacterSheet character)
        {
            // TODO: Move to generator / assigner / selector
            var ability = EnumHelpers.ChooseOne<AbilityScoreTypes>();
            var adjust = new AbilityScoreAdjustment();
            adjust.Reason = "Level Up";
            adjust.Modifier = 1;
            adjust.AbilityName = ability;

            character.AbilityScores.GetAbility(ability).AddModifier(adjust);
        }

        private void AddSpecialAbilities(CharacterSheet character, Level level)
        {
            character.ProcessSpecialAbilities(level);
        }
    }
}
