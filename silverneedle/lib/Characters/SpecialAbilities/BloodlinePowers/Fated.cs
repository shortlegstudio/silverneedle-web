// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities.BloodlinePowers
{
    using SilverNeedle.Utility;
    public class Fated : IAbility, INameByType, IBloodlinePower, IComponent
    {
        public ComponentContainer Parent { get; set; }
        private ClassLevel sorcerer;
        public void Initialize(ComponentContainer components)
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

        public string DisplayString()
        {
            return "{0} ({1} AC & Saves during surprise rounds".Formatted(this.Name(), this.Bonus.ToModifierString());
        }

    }
}