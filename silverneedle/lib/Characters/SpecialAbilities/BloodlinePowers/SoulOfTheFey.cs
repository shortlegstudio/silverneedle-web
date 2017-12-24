// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities.BloodlinePowers
{
    using SilverNeedle.Utility;

    public class SoulOfTheFey : SpecialAbility, IBloodlinePower, IComponent
    {
        public void Initialize(ComponentContainer components)
        {
            var defense = components.Get<DefenseStats>();
            defense.AddImmunity("poison");
            defense.AddDamageResistance(new DamageResistance(10, "cold iron"));

            components.Add(new SpellBasedAbility("Shadow Walk", 1));
        }
    }
}