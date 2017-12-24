// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities.BloodlinePowers
{
    using SilverNeedle.Utility;
    public class ElementalResistance : SpecialAbility, IBloodlinePower, IComponent, IImprovesWithLevels
    {
        private DamageResistance resistance; 
        private ClassLevel sorcerer;
        public void Initialize(ComponentContainer components)
        {
            sorcerer = components.Get<ClassLevel>();
            var elementType = components.Get<ElementalType>();
            resistance = new DamageResistance(10, elementType.EnergyType);
            var defense = components.Get<DefenseStats>();
            defense.AddDamageResistance(resistance);
        }

        public void LeveledUp(ComponentContainer components)
        {
            if(sorcerer.Level == 9)
                resistance.Amount = 20;
        }
    }
}