// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    /// <summary>
    /// Represents the ability to allocate ability points to an attribute of 
    /// the character's choosing. 
    /// </summary>
    public class AbilityScoreToken
    {
        private int amount;
        private string source;

        public AbilityScoreToken(int amount, string source)
        {
            this.amount = amount;
            this.source = source;
        }


        public AbilityScoreAdjustment CreateAdjustment(AbilityScoreTypes abilityScore)
        {
            var modifier = new AbilityScoreAdjustment();
            modifier.AbilityName = abilityScore;
            modifier.Modifier = amount;
            modifier.Reason = source;
            return modifier;
        }

    }
}