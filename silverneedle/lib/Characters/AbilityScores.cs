// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    using System;
    using System.Collections.Generic;
    using SilverNeedle;
    using SilverNeedle.Utility;

    /// <summary>
    /// A container that manages all the ability scores for a character
    /// </summary>
    public class AbilityScores : IComponent
    {
        public ComponentContainer Parent { get; set; }
        /// <summary>
        /// The abilities for this character
        /// </summary>
        private Dictionary<AbilityScoreTypes, AbilityScore> abilities;

        /// <summary>
        /// Initializes a new instance of the <see cref="SilverNeedle.Characters.AbilityScores"/> class.
        /// </summary>
        public AbilityScores()
        {
            this.FillAbilities();
        }

        /// <summary>
        /// Gets the abilities.
        /// </summary>
        /// <returns>The abilities.</returns>
        public IEnumerable<AbilityScore> Abilities 
        {
            get { return this.abilities.Values; }
        }

        /// <summary>
        /// Gets the ability. This throws an exception if ability is not found
        /// </summary>
        /// <returns>The ability that was looked up.</returns>
        /// <param name="ability">Ability to find.</param>
        public AbilityScore GetAbility(AbilityScoreTypes ability)
        {
            return this.abilities[ability];
        }

        /// <summary>
        /// Gets the score for an ability
        /// </summary>
        /// <returns>The ability score.</returns>
        /// <param name="ability">Ability to lookup.</param>
        public int GetScore(AbilityScoreTypes ability)
        {
            return this.abilities[ability].TotalValue;
        }

        /// <summary>
        /// Gets the score for an ability
        /// </summary>
        /// <returns>The ability score.</returns>
        /// <param name="ability">Ability to lookup.</param>
        public int GetScore(string ability)
        {
            return this.GetScore(AbilityScore.GetType(ability));
        }

        /// <summary>
        /// Sets the score for an ability
        /// </summary>
        /// <param name="ability">Ability score.</param>
        /// <param name="val">Value for the new ability score.</param>
        public void SetScore(AbilityScoreTypes ability, int val)
        {
            this.abilities[ability].SetValue(val);
        }

        /// <summary>
        /// Gets the modifier for an ability
        /// </summary>
        /// <returns>The modifier score for the ability.</returns>
        /// <param name="ability">Ability to find</param>
        public int GetModifier(AbilityScoreTypes ability)
        {
            return this.abilities[ability].TotalModifier;
        }

        public AbilityStatModifier GetStatModifier(AbilityScoreTypes ability)
        {
            return GetAbility(ability).UniversalStatModifier;
        }

        /// <summary>
        /// Copies these ability scores from another container for duplication
        /// </summary>
        /// <param name="scores">AbilityScores container to copy values to</param>
        public void Copy(AbilityScores scores)
        {
            foreach (var e in scores.Abilities)
            {
                this.SetScore(e.Ability, e.BaseValue);
            }
        }

        /// <summary>
        /// Fills the abilities collection with zeroed out scores
        /// </summary>
        private void FillAbilities()
        {
            this.abilities = new Dictionary<AbilityScoreTypes, AbilityScore>();
            foreach (var v in EnumHelpers.GetValues<AbilityScoreTypes>())
            {
                var ability = new AbilityScore(v, 0);
                this.abilities.Add(v, ability);
            }
        }

        public void Initialize(ComponentContainer components)
        {
            foreach(var abl in this.abilities)
                components.Add(abl.Value);
        }
    }
}