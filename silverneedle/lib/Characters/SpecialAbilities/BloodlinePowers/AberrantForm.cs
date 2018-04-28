// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Characters.SpecialAbilities.BloodlinePowers
{
    using SilverNeedle.Characters.Senses;
    using SilverNeedle.Utility;
    public class AberrantForm : AbilityDisplayAsName, IBloodlinePower, IComponent
    {
        public ComponentContainer Parent { get; set; }
        public void Initialize(ComponentContainer components)
        {
            var def = components.Get<DefenseStats>();
            def.AddImmunity("Criticals");
            def.AddImmunity("Sneak Attacks");

            def.AddDamageResistance(new EnergyResistance(5, "-"));
            components.Add(new Blindsight(50));
        }
    }
}