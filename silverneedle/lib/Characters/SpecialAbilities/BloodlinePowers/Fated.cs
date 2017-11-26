// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities.BloodlinePowers
{
    using SilverNeedle.Utility;
    public class Fated : SpecialAbility, IBloodlinePower, IComponent
    {
        private ClassLevel sorcerer;
        public void Initialize(ComponentBag components)
        {
            sorcerer = components.Get<ClassLevel>();
        }
        public int Bonus
        {
            get
            {
                return 1 + (sorcerer.Level - 3) / 4;
            }
        }

        public override string Name
        {
            get 
            {
                return "{0} ({1} AC & Saves during surprise rounds".Formatted(base.Name, this.Bonus.ToModifierString());
            }
        }

    }
}