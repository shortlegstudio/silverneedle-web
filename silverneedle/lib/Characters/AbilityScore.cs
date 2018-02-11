// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    using SilverNeedle;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    /// <summary>
    /// An ability score for a character. Examples: Strength, Intelligence, Charisma, ...
    /// </summary>
    public class AbilityScore : BasicStat, IValueStatistic, IComponent
    {
        private BasicStat abilityModifier;
        public AbilityScore(AbilityScoreTypes type, int val)
            : base(type.ToString(), val)
        {
            this.Ability = type;
            Setup();
        }

        public AbilityScore(IObjectStore configuration) : base(configuration)
        {
            this.Ability = this.Name.EnumValue<AbilityScoreTypes>();
            Setup();
        }

        private void Setup()
        {
            this.abilityModifier = new BasicStat("{0}-modifier".Formatted(Ability).ToLower());
            this.abilityModifier.AddModifier(
                new DelegateStatModifier(
                    this.abilityModifier.Name,
                    "calculation",
                    () => CalculateModifier(this.TotalValue))
            );
            this.UniversalStatModifier = new AbilityStatModifier(this);
        }


        /// <summary>
        /// Gets or sets the name. Synonomous with AbilityScoreTypes
        /// </summary>
        /// <value>The name of the ability</value>
        public AbilityScoreTypes Ability { get; private set; }

        /// <summary>
        /// Gets the total modifier.
        /// </summary>
        /// <value>The total modifier.</value>
        public int TotalModifier
        {
            get
            {
                return abilityModifier.TotalValue;
            }
        }

        public IValueStatistic ModifierStat { get { return abilityModifier; } }

        /// <summary>
        /// Gets the type of ability score
        /// </summary>
        /// <returns>The type.</returns>
        /// <param name="name">Name of the AbilityScoreType to parse</param>
        public static AbilityScoreTypes GetType(string name)
        {
            return (AbilityScoreTypes)System.Enum.Parse(typeof(AbilityScoreTypes), name, true);
        }

        /// <summary>
        /// Calculates the modifier. Formula is: (val / 2) -5
        /// </summary>
        /// <returns>The modifier.</returns>
        /// <param name="val">The score to use to calculate the modifier</param>
        public static int CalculateModifier(int val)
        {
            return (val / 2) - 5;
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents the current <see cref="SilverNeedle.Characters.AbilityScore"/>.
        /// </summary>
        /// <returns>A <see cref="System.String"/> that represents the current <see cref="SilverNeedle.Characters.AbilityScore"/>.</returns>
        public override string ToString()
        {
            return string.Format(
                "[AbilityScore: Name={0}, Adjustments={1}, BaseValue={2}, TotalValue={3}, TotalModifier={4}, SumAdjustments={5}]", 
                this.Ability, 
                this.Modifiers, 
                this.BaseValue, 
                this.TotalValue, 
                this.TotalModifier, 
                this.SumBasicModifiers());
        }

        public void Initialize(ComponentContainer components)
        {
            components.Add(this.abilityModifier);
        }

        public AbilityStatModifier UniversalStatModifier { get; private set; }
    }
}