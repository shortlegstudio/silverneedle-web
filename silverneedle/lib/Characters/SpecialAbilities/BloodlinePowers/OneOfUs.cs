// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities.BloodlinePowers
{
    using SilverNeedle.Utility;

    public class OneOfUs : SpecialAbility, IBloodlinePower, IComponent
    {
        public void Initialize(ComponentBag components)
        {
            var defense = components.Get<DefenseStats>();
            defense.AddImmunity("cold");
            defense.AddImmunity("nonlethal");
            defense.AddImmunity("paralysis");
            defense.AddImmunity("sleep");
            defense.AddDamageResistance(new DamageResistance(5, "-"));

            var modifier = new ConditionalStatModifier(
                new ValueStatModifier(4, this.Name),
                "undead spells/abilities"
            );

            defense.FortitudeSave.AddModifier(modifier);
            defense.ReflexSave.AddModifier(modifier);
            defense.WillSave.AddModifier(modifier);
        }
    }
}