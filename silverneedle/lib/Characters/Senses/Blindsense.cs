// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Characters.Senses
{
    using SilverNeedle.Utility;
    public class Blindsense : SpecialAbilities.SpecialAbility
    {
        public Blindsense(int rangeInFeet) : base("Blindsense ({0})".Formatted(rangeInFeet.ToRangeString()), "Sight")
        {

        }
    }
}