//-----------------------------------------------------------------------
// <copyright file="AverageAbilityScoreGenerator.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SilverNeedle.Actions.CharacterGenerator.Abilities
{
    using SilverNeedle;
    using SilverNeedle.Characters;
    using SilverNeedle.Utility;

    /// <summary>
    /// Generates ability scores with just your basic average score. Useful for a base line or to
    /// make sure things are initialized properly
    /// </summary>
    public class AverageAbilityScoreGenerator : ICharacterDesignStep
    {
        /// <summary>
        /// The average score.
        /// </summary>
        private const int AverageScore = 10;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="SilverNeedle.Actions.CharacterGenerator.Abilities.AverageAbilityScoreGenerator"/> class.
        /// </summary>
        public AverageAbilityScoreGenerator()
        {
        }

        public void Process(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            foreach (var e in EnumHelpers.GetValues<AbilityScoreTypes>())
            {
                character.AbilityScores.SetScore(e, AverageScore);
            }
        }
    }
}