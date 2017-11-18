// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Senses
{
    public class Blindsight : SpecialAbilities.SpecialAbility
    {
        public Blindsight(int rangeInFeet) : base("Blindsight ({0} ft)".Formatted(rangeInFeet), "Sight")
        {

        }
    }
}