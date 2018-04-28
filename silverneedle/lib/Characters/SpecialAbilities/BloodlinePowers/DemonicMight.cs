// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities.BloodlinePowers
{
    using SilverNeedle.Utility;
    public class DemonicMight : AbilityDisplayAsName, IComponent, IBloodlinePower
    {
        public ComponentContainer Parent { get; set; }
        public void Initialize(ComponentContainer components)
        {
            var defense = components.Get<DefenseStats>();
            defense.AddImmunity("electricity");
            defense.AddImmunity("poison");

            //Damage resistances
            defense.AddDamageResistance(new EnergyResistance(10, "acid"));
            defense.AddDamageResistance(new EnergyResistance(10, "cold"));
            defense.AddDamageResistance(new EnergyResistance(10, "fire"));
        }
    }
}