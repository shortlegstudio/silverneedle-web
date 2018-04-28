// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities.BloodlinePowers
{
    using SilverNeedle.Utility;
    public class ElementalResistance : AbilityDisplayAsName, IBloodlinePower, IComponent, IImprovesWithLevels
    {
        public ComponentContainer Parent { get; set; }
        private EnergyResistance resistance; 
        private ClassLevel sorcerer;
        public void Initialize(ComponentContainer components)
        {
            sorcerer = components.Get<ClassLevel>();
            var elementType = components.Get<ElementalType>();
            resistance = new EnergyResistance(10, elementType.EnergyType);
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