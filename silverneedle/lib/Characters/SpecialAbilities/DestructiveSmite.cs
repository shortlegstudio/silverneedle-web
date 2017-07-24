// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    public class DestructiveSmite : SpecialAbility
    {
        private AbilityScore wisdom;
        public DestructiveSmite(AbilityScore wisdom)
        {
            this.wisdom = wisdom;
        }

        public int UsesPerDay
        {
            get { return 3 + wisdom.TotalModifier; }
        }
    }
}