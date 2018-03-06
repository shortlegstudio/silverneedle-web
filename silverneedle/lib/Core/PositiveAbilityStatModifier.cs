// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle
{
    using SilverNeedle.Characters;
    public class PositiveAbilityStatModifier : AbilityStatModifier
    {
        public PositiveAbilityStatModifier(AbilityScore ability) : base(ability)
        {

        }

        public override float Modifier 
        {
            get
            {
                return System.Math.Max(0, base.Modifier);
            }
        }
    }
}