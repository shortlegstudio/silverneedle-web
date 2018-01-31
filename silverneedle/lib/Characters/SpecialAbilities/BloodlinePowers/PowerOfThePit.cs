// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities.BloodlinePowers
{
    using SilverNeedle.Utility;

    public class PowerOfThePit : AbilityDisplayAsName, IBloodlinePower, IComponent
    {
        public void Initialize(ComponentContainer components)
        {
            var defense = components.Get<DefenseStats>();
            defense.AddImmunity("fire");
            defense.AddImmunity("poison");
            defense.AddDamageResistance(new EnergyResistance(10, "acid"));
            defense.AddDamageResistance(new EnergyResistance(10, "cold"));
            components.Add(new Senses.PerfectDarkvision(60));

        }
    }
}