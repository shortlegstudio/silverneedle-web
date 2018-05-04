﻿// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using SilverNeedle.Utility;

    /// <summary>
    /// Character skills represent the character's ability in a specific skill set. This tracks
    /// the abilities and ranks applied to a skill. It also is aware of any modifiers that might happen.
    /// </summary>
    public class CharacterSkill : IValueStatistic
    {
        /// <summary>
        /// The base ability score that this skill is based on.
        /// </summary>
        private AbilityScore baseAbilityScore;

        /// <summary>
        /// The skill stats that track modifiers and final scores.
        /// </summary>
        private BasicStat skillStats;
        private bool bypassTraining;

        public CharacterSkill() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SilverNeedle.Characters.CharacterSkill"/> class.
        /// </summary>
        /// <param name="baseSkill">Base skill.</param>
        /// <param name="baseScore">Base score.</param>
        /// <param name="isClassSkill">If set to <c>true</c> is class skill.</param>
        public CharacterSkill(Skill baseSkill, AbilityScore baseScore, bool isClassSkill)
        {
            Skill = baseSkill;
            this.baseAbilityScore = baseScore;
            this.ClassSkill = isClassSkill;
            this.skillStats = new BasicStat(baseSkill.Name);
        }

        /// <summary>
        /// Gets the skill this stat is based on.
        /// </summary>
        /// <value>The base skill.</value>
        public Skill Skill { get; private set; }

        public bool ArmorCheckPenalty { get { return this.Skill.UseArmorCheckPenalty; } }

        /// <summary>
        /// Gets the ranks added to this skill.
        /// </summary>
        /// <value>The ranks added.</value>
        public int Ranks 
        { 
            get { return this.skillStats.BaseValue; } 
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="SilverNeedle.Characters.CharacterSkill"/> is a
        /// class skill.
        /// </summary>
        /// <value><c>true</c> if class skill; otherwise, <c>false</c>.</value>
        public bool ClassSkill { get; set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="SilverNeedle.Characters.CharacterSkill"/> 
        /// is able for the character to use.
        /// Training Required skills need at least one rank.
        /// </summary>
        /// <value><c>true</c> if able to use; otherwise, <c>false</c>.</value>
        public bool AbleToUse
        { 
            get
            {
                return !Skill.TrainingRequired || this.Ranks > 0 || this.bypassTraining;
            } 
        }

        /// <summary>
        /// Gets the name of the skill.
        /// </summary>
        /// <value>The skill name.</value>
        public string Name
        { 
            get { return Skill.Name; }
        }

        /// <summary>
        /// Gets the conditionals modifiers associated with this skill.
        /// </summary>
        /// <value>The conditional modifiers.</value>
        public IEnumerable<string> ConditionalModifiers
        {
            get
            {
                return this.skillStats.GetConditions();
            }
        }

        public int TotalValue { get { return Score(); } }

        /// <summary>
        /// Calculates the score of this skill using all available modifiers.
        /// </summary>
        /// <returns>The ability score for this skill.</returns>
        public int Score()
        {
            return this.Score(string.Empty);
        }

        private int Score(string condition)
        {
            // I can't do this...
            if (!this.AbleToUse)
            {
                return int.MinValue;
            }

            var val = this.baseAbilityScore.TotalModifier;

            // Class Skill
            if (this.Ranks > 0 && this.ClassSkill)
            {
                val += 3;
            }

            // Other Bonuses and Ranks
            val += this.skillStats.GetConditionalValue(condition);
            return val;
        }


        /// <summary>
        /// Adds a skill rank into this skill.
        /// </summary>
        public void AddRank()
        {
            this.skillStats.SetValue(this.skillStats.BaseValue + 1);
        }

        public IEnumerable<IStatisticModifier> Modifiers { get { return this.skillStats.Modifiers; } }

        /// <summary>
        /// Adds a stat modifier to the skill.
        /// </summary>
        /// <param name="modifier">Modifier for the skill.</param>
        public void AddModifier(IStatisticModifier modifier)
        {
            this.skillStats.AddModifier(modifier);
        }

        public void RemoveModifier(IStatisticModifier modifier)
        {
            this.skillStats.RemoveModifier(modifier);
        }

        /// <summary>
        /// Gets the conditional score.
        /// </summary>
        /// <returns>The conditional score.</returns>
        /// <param name="condition">Condition to score the skill in.</param>
        public int GetConditionalValue(string condition)
        {
            return this.Score(condition);
        }

        public IEnumerable<string> GetConditions()
        {
            return this.skillStats.GetConditions();
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents the current <see cref="SilverNeedle.Characters.CharacterSkill"/>.
        /// </summary>
        /// <returns>A <see cref="System.String"/> that represents the current <see cref="SilverNeedle.Characters.CharacterSkill"/>.</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("{0} {1}", this.Name, this.Score().ToModifierString());

            var mods = this.ConditionalModifiers.Select(
                x => string.Format("{0} {1}", this.GetConditionalValue(x).ToModifierString(), x));

            if (mods.Count() > 0)
            {
                sb.AppendFormat(
                    " ({0})",
                    string.Join(",", mods.ToArray()));
            }

            return sb.ToString();
        }

        public void CanUseWithoutTraining()
        {
            this.bypassTraining = true;
        }

        public bool Matches(string name)
        {
            return this.Name.EqualsIgnoreCase(name);
        }
    }
}