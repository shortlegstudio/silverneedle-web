// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    public class TouchOfEvil
    {

        AbilityScore wisdom;
        public int UsesPerDay
        {
            get
            {
                return 3 + wisdom.TotalModifier;
            }
        }

        public TouchOfEvil(AbilityScore wisdom)
        {
            this.wisdom = wisdom;
        }
    }
}