// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using SilverNeedle.Utility;
    public class StillMind : SpecialAbility, IComponent
    {
        private IStatModifier enchantModifier;
        public void Initialize(ComponentContainer components)
        {
            enchantModifier = new ConditionalStatModifier(
                new ValueStatModifier(2, "bonus"),
                "enchantment"
            );
            var def = components.Get<DefenseStats>();
            def.FortitudeSave.AddModifier(enchantModifier);
            def.ReflexSave.AddModifier(enchantModifier);
            def.WillSave.AddModifier(enchantModifier);
        }
    }
}