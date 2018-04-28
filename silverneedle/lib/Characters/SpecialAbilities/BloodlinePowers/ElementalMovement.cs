// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities.BloodlinePowers
{
    using SilverNeedle.Utility;

    public class ElementalMovement : IAbility, INameByType, IBloodlinePower, IComponent
    {
        public ComponentContainer Parent { get; set; }
        ElementalType elementalType;
        public void Initialize(ComponentContainer components)
        {
            elementalType = components.Get<ElementalType>();
        }

        public string DisplayString()
        {
            return "{0} ({1} {2})".Formatted(
                this.Name(),
                elementalType.MovementSpeed.ToRangeString(),
                elementalType.MovementType
            );
        }
    }
}