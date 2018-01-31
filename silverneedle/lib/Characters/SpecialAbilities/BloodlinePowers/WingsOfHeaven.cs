// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Characters.SpecialAbilities.BloodlinePowers
{
    using SilverNeedle.Utility;
    public class WingsOfHeaven : IAbility, INameByType, IBloodlinePower, IComponent
    {
        private ClassLevel sorcererLevel;
        private ComponentContainer components;
        private bool IsAscended()
        {
            return components.Contains<Ascension>();
        }

        public void Initialize(ComponentContainer components)
        {
            this.components = components;
            sorcererLevel = components.Get<ClassLevel>();
        }

        public int MinutesPerDay 
        { 
            get 
            { 
                if(IsAscended())
                    return int.MaxValue;
                return sorcererLevel.Level; 
            } 
        }

        public string DisplayString()
        {
            return "{0} ({1} minutes/day)".Formatted(this.Name(), IsAscended() ? "unlimited" : MinutesPerDay.ToString()); 
        }
    }
}