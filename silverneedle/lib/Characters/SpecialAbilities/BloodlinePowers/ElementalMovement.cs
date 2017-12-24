// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities.BloodlinePowers
{
    using SilverNeedle.Utility;

    public class ElementalMovement : SpecialAbility, IBloodlinePower, IComponent
    {
        ElementalType elementalType;
        public void Initialize(ComponentContainer components)
        {
            elementalType = components.Get<ElementalType>();
            this.Name = "{0} ({1} {2})".Formatted(
                base.Name,
                elementalType.MovementSpeed.ToRangeString(),
                elementalType.MovementType
            );
        }
    }
}