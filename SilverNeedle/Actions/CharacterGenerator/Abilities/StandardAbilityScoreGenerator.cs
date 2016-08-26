//-----------------------------------------------------------------------
// <copyright file="StandardAbilityScoreGenerator.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SilverNeedle.Mechanics.CharacterGenerator.Abilities
{
    using SilverNeedle;
    using SilverNeedle.Dice;
    using SilverNeedle.Characters;

    /// <summary>
    /// Generates ability scores by rolling 4d6 for each and selecting the top 3 dice
    /// </summary>
    public class StandardAbilityScoreGenerator : IAbilityScoreGenerator
    {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="SilverNeedle.Mechanics.CharacterGenerator.Abilities.StandardAbilityScoreGenerator"/> class.
        /// </summary>
        public StandardAbilityScoreGenerator()
        {
        }

        /// <summary>
        /// Assigns the abilities.
        /// </summary>
        /// <param name="abilities">Abilities to assign to</param>
        public void AssignAbilities(AbilityScores abilities)
        {
            foreach (var e in EnumHelpers.GetValues<AbilityScoreTypes>())
            {
                abilities.SetScore(e, this.Roll4d6());
            }
        }

        /// <summary>
        /// Roll4d6 To get ability Score
        /// </summary>
        /// <returns>Top 3 dice summed</returns>
        private int Roll4d6()
        {
            var diceCup = new Cup(Die.GetDice(DiceSides.d6, 4));
            diceCup.Roll();
            return diceCup.SumTop(3);
        }
    }
}