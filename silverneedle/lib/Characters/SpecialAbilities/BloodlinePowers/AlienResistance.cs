// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities.BloodlinePowers
{
    using SilverNeedle.Utility;

    public class AlienResistance : AbilityDisplayAsName, IBloodlinePower, IComponent
    {
        public ComponentContainer Parent { get; set; }
        private IValueStatModifier spellResistance;
        public void Initialize(ComponentContainer components)
        {
            var classLevel = components.Get<ClassLevel>();
            spellResistance = new DelegateStatModifier(
                "Spell Resistance",
                "base",
                () => { return 10 + classLevel.Level; }
            );
            var def = components.Get<DefenseStats>();
            def.SpellResistance.AddModifier(spellResistance);
        }
    }
}