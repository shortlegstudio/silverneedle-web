// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities.BloodlinePowers
{
    using SilverNeedle.Utility;
    public class DemonicMight : SpecialAbility, IComponent, IBloodlinePower
    {
        public void Initialize(ComponentBag components)
        {
            var defense = components.Get<DefenseStats>();
            defense.AddImmunity("electricity");
            defense.AddImmunity("poison");

            //Damage resistances
            defense.AddDamageResistance(new DamageResistance(10, "acid"));
            defense.AddDamageResistance(new DamageResistance(10, "cold"));
            defense.AddDamageResistance(new DamageResistance(10, "fire"));
        }
    }
}