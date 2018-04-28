// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities.BloodlinePowers
{
    using SilverNeedle.Utility;

    public class OneOfUs : AbilityDisplayAsName, IBloodlinePower, IComponent
    {
        public ComponentContainer Parent { get; set; }
        public void Initialize(ComponentContainer components)
        {
            var defense = components.Get<DefenseStats>();
            defense.AddImmunity("cold");
            defense.AddImmunity("nonlethal");
            defense.AddImmunity("paralysis");
            defense.AddImmunity("sleep");
            components.Add(new DamageReduction("-", 5));

            var modifier = new ConditionalStatModifier(
                new ValueStatModifier(4),
                "undead spells/abilities"
            );

            defense.FortitudeSave.AddModifier(modifier);
            defense.ReflexSave.AddModifier(modifier);
            defense.WillSave.AddModifier(modifier);
        }
    }
}