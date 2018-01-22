// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    using SilverNeedle.Serialization;
    /// <summary>
    /// Represents the ability to allocate ability points to an attribute of 
    /// the character's choosing. 
    /// </summary>
    public class AbilityScoreToken
    {
        private int amount;
        private string source;
        private string type;

        public AbilityScoreToken(int amount, string source)
        {
            this.amount = amount;
            this.source = source;
        }

        public AbilityScoreToken(IObjectStore configuration)
        {
            this.amount = configuration.GetInteger("modifier");
            this.type = configuration.GetString("modifier-type");
            this.source = configuration.GetString("modifier-type");
        }


        public IValueStatModifier CreateAdjustment(AbilityScoreTypes abilityScore)
        {
            return new ValueStatModifier(
                abilityScore.ToString(),
                this.amount,
                this.type
            );
        }

    }
}