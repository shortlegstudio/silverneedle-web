// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    public class TouchOfDarkness : SpecialAbility
    {
        public int UsesPerDay
        {
            get
            {
                return 3 + wisdom.TotalModifier;
            }
        }

        private AbilityScore wisdom;
        public TouchOfDarkness(AbilityScore wisdom)
        {
            this.wisdom = wisdom;
        }
    }
}