// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities.BloodlinePowers
{
    using SilverNeedle.Characters.Senses;
    using SilverNeedle.Utility;
    public class PowerOfWyrms : AbilityDisplayAsName, IBloodlinePower, IComponent
    {
        public ComponentContainer Parent { get; set; }
        public void Initialize(ComponentContainer components)
        {
            var draconic = components.Get<IDraconicBloodline>();
            var dragonType = draconic.DragonType;
            var defense = components.Get<DefenseStats>();
            defense.AddImmunity("paralysis");
            defense.AddImmunity("sleep");
            defense.AddImmunity(dragonType.EnergyType);

            components.Add(new Blindsense(60));

        }
    }
}