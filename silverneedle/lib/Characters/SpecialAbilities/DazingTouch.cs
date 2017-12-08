// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    public class DazingTouch : SpecialAbility
    {
        private AbilityScore wisdom;
        public int UsesPerDay
        {
            get { return 3 + wisdom.TotalModifier; }
        }

        public DazingTouch(AbilityScore wisdom)
        {
            this.wisdom = wisdom;
        }

        public DazingTouch() { }
    }
}