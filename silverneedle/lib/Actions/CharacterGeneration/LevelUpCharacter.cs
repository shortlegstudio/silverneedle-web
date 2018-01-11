// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.CharacterGeneration
{
    using System;
    using SilverNeedle.Characters;

    /// <summary>
    /// Level up generator handles adding more levels to a character in a specific class
    /// </summary>
    public class LevelUpCharacter : ICharacterDesignStep
    {
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
            character.Offense.LevelUp(character.Class);            
            character.Defense.LevelUpDefenseStats(character.Class);
            var level = character.Class.GetLevel(character.Level);
            character.Add(level);

            if(character.Level % 2 == 1) {
                character.Add(new FeatToken());
            }

            // Special Level ups
            if (character.Level % 4 == 0)
            {
                character.Add(new AbilityScoreToken(1, string.Format("Level ({0})", character.Level)));
            }
        }

        public void ExecuteStep(CharacterSheet character)
        {
            LevelUp(character);
        }
    }
}
