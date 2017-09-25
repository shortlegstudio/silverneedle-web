// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using SilverNeedle.Characters;
    using SilverNeedle.Utility;

    /// <summary>
    /// Represents any kind of character stat that can be a pure number. It allows
    /// for tracking when the stat changes through events. It also provides the ability
    /// to specify conditional modifiers that can be used in certain circumstances to
    /// change the stat value.
    /// </summary>
    public class BasicStat : IStatistic
    {
        public string Name { get; private set; }
        /// <summary>
        /// Tracks all modifiers associated with this stat
        /// </summary>
        private IList<IStatModifier> statModifiers;

        /// <summary>
        /// The conditional modifiers. This is a HACK and should be handled more gracefully
        /// </summary>
        private IList<ConditionalStatModifier> conditionalModifiers;

        /// <summary>
        /// Initializes a new instance of the <see cref="SilverNeedle.BasicStat"/> class.
        /// </summary>
        public BasicStat(string name)
        {
            this.Name = name; 
            this.statModifiers = new List<IStatModifier>();
            this.conditionalModifiers = new List<ConditionalStatModifier>();
            this.Maximum = 123456789; //Set default max to weird number in case it comes into play in the future
            this.Minimum = -123456789; //Set default max to weird number in case it comes into play in the future
            this.UseModifierString = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SilverNeedle.BasicStat"/> class.
        /// </summary>
        /// <param name="baseValue">Base value of the stat</param>
        public BasicStat(string name, int baseValue)
            : this(name)
        {
            this.BaseValue = baseValue;
        }

        /// <summary>
        /// Gets or sets the base value of the stat
        /// </summary>
        /// <value>The base value.</value>
        public int BaseValue { get; protected set; }

        /// <summary>
        /// Gets the total value of the stat. This includes all active modifiers. These are modifiers
        /// without a conditional element
        /// </summary>
        /// <value>The total value of the stat</value>
        public int TotalValue 
        { 
            get 
            {
                return CalculateTotalValue();
            } 
        }

        public bool UseModifierString { get; set; }

        public int Maximum { get; set; }
        public int Minimum { get; set; }

        /// <summary>
        /// Gets an enumerable list of modifiers associated with this stat
        /// </summary>
        /// <value>The modifiers.</value>
        public IEnumerable<IStatModifier> Modifiers 
        { 
            get 
            { 
                return this.statModifiers; 
            } 
        }

        /// <summary>
        /// Gets the sum basic modifiers. These are modifiers without a conditional element
        /// </summary>
        /// <value>The sum basic modifiers.</value>
        public virtual int SumBasicModifiers()
        {
            return this.statModifiers.Sum(x => x.Modifier).FloorToInt(); 
        }

        /// <summary>
        /// Adds a modifier to the stat
        /// </summary>
        /// <param name="modifier">Modifier for the stat.</param>
        /// <remarks>Triggers a modified event even if the Total Value does not change</remarks>
        public void AddModifier(IStatModifier modifier)
        {
            // HACK: This is a hack job I think. Shouldn't check type to get proper behavior
            if (modifier is ConditionalStatModifier)
            {
                this.AddModifier((ConditionalStatModifier)modifier);
                return;
            }

            this.statModifiers.Add(modifier);
        }

        /// <summary>
        /// Adds a conditional modifier
        /// </summary>
        /// <param name="conditional">Conditional modifier to add.</param>
        public void AddModifier(ConditionalStatModifier conditional)
        {
            this.conditionalModifiers.Add(conditional);
        }

        public void AddModifiers(params IStatModifier[] modifiers)
        {
            foreach(var mod in modifiers)
            {
                AddModifier(mod);
            }
        }

        public void AddModifiers(IEnumerable<IStatModifier> modifiers)
        {
            foreach(var mod in modifiers)
            {
                this.AddModifier(mod);
            }
        }

        /// <summary>
        /// Sets the Base Value for the stat 
        /// </summary>
        /// <param name="val">New base value</param>
        public void SetValue(int val)
        {
            this.BaseValue = val;
        }

        /// <summary>
        /// Gets all the different conditions associated with this stat
        /// </summary>
        /// <returns>The conditions available to the stat</returns>
        public IEnumerable<string> GetConditions()
        {
            return this.conditionalModifiers.GroupBy(x => x.Condition).Select(x => x.Key);
        }

        /// <summary>
        /// Gets the conditional version of the stat.
        /// </summary>
        /// <returns>The conditional score.</returns>
        /// <param name="condition">Condition to calculate from</param>
        public int GetConditionalValue(string condition)
        {
            var conditions = this.conditionalModifiers.Where(x => x.Condition == condition);
            return this.TotalValue + (int)conditions.Sum(x => x.Modifier);
        }

        /// <summary>
        /// Tos the string. TODO: Why do stats not have names?
        /// </summary>
        /// <returns>The string.</returns>
        /// <param name="statName">Stat name.</param>
        public string ToString(string statName)
        {
            if(UseModifierString)
                return ToStringWithModifier(statName);
            
            return ToStringFlat(statName);
        }

        public string ToStringWithModifier(string statName)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("{0} {1}", statName, this.TotalValue.ToModifierString());

            var mods = this.GetConditions().Select(
                  x => string.Format("{0} {1}", this.GetConditionalValue(x).ToModifierString(), x));

            if (mods.Count() > 0)
            {
                sb.AppendFormat(
                    " ({0})",
                    string.Join(",", mods.ToArray()));
            }

            return sb.ToString();
        }

        public string ToStringFlat(string statName)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("{0} {1}", statName, this.TotalValue);

            var mods = this.GetConditions().Select(
                  x => string.Format("{0} {1}", this.GetConditionalValue(x), x));

            if (mods.Count() > 0)
            {
                sb.AppendFormat(
                    " ({0})",
                    string.Join(",", mods.ToArray()));
            }

            return sb.ToString();
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents the current <see cref="SilverNeedle.BasicStat"/>.
        /// </summary>
        /// <returns>A <see cref="System.String"/> that represents the current <see cref="SilverNeedle.BasicStat"/>.</returns>
        public override string ToString()
        {
            return this.ToString(string.Empty);
        }

        protected virtual int CalculateTotalValue()
        {
            return Math.Max(Minimum, Math.Min(this.Maximum, this.BaseValue + this.SumBasicModifiers())); 
        }
    }
}