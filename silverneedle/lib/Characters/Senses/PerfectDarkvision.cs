// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Senses
{
    using SilverNeedle.Utility;
    public class PerfectDarkvision : SpecialAbilities.SpecialAbility
    {
        public PerfectDarkvision(int rangeInFeet) : base("Perfect Darkvision ({0})".Formatted(rangeInFeet.ToRangeString()), "Sight")
        {

        }
    }
}