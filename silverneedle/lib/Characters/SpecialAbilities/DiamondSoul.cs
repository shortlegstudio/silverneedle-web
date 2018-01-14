// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using SilverNeedle.Utility;
    public class DiamondSoul : SpecialAbility, IComponent
    {
        private IValueStatModifier spellResistanceModifier;
        private ClassLevel monkLevel;
        public void Initialize(ComponentContainer components)
        {
            monkLevel = components.Get<ClassLevel>();
            spellResistanceModifier = new DelegateStatModifier(
                StatNames.SpellResistance,
                "Spell Resistance",
                this.Name,
                Modifier
            );
            components.ApplyStatModifier(spellResistanceModifier);
        }

        private float Modifier()
        {
            return 10 + monkLevel.Level;
        }
    }
}