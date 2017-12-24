// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities.BloodlinePowers
{
    using SilverNeedle.Utility;

    public class ElementalBody : SpecialAbility, IBloodlinePower, IComponent
    {
        public void Initialize(ComponentContainer components)
        {
            var defense = components.Get<DefenseStats>();
            var elementalType = components.Get<ElementalType>();
            defense.AddImmunity("sneak attacks");
            defense.AddImmunity("critical hits");
            defense.AddImmunity(elementalType.EnergyType);
        }
    }
}