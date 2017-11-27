// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities.BloodlinePowers
{
    using SilverNeedle.Characters.Senses;
    using SilverNeedle.Utility;
    public class PowerOfWyrms : SpecialAbility, IBloodlinePower, IComponent
    {
        public void Initialize(ComponentBag components)
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