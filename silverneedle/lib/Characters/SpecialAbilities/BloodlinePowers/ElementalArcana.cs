// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities.BloodlinePowers
{
    using SilverNeedle.Utility;
    public class ElementalArcana : BloodlineArcana, IComponent
    {
        private ElementalType elementalType;

        public override string BonusAbility { get { return "change energy damage spells to {0}".Formatted(elementalType.EnergyType); } }

        public void Initialize(ComponentBag components)
        {
            elementalType = components.Get<ElementalType>();
        }
    }
}