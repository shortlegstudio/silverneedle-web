// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities.BloodlinePowers
{
    using SilverNeedle.Serialization;
    using SilverNeedle.Spells;
    using SilverNeedle.Utility;

    public class SchoolPower : IAbility, INameByType, IBloodlinePower, IComponent
    {
        public School School { get; private set; } 

        public void Initialize(ComponentContainer components)
        {
            this.School = GatewayProvider.All<School>().ChooseOne();
        }

        public string DisplayString() 
        {
            return "{0} (+2 DC for {1} spells)".Formatted(this.Name(), this.School.Name); 
        }

    }
}