// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities.BloodlinePowers
{
    using SilverNeedle.Utility;

    public class PowerOfThePit : SpecialAbility, IBloodlinePower, IComponent
    {
        public void Initialize(ComponentBag components)
        {
            var defense = components.Get<DefenseStats>();
            defense.AddImmunity("fire");
            defense.AddImmunity("poison");
            defense.AddDamageResistance(new DamageResistance(10, "acid"));
            defense.AddDamageResistance(new DamageResistance(10, "cold"));
            components.Add(new Senses.PerfectDarkvision(60));

        }
    }
}