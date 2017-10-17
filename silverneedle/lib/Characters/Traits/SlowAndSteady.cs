// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Traits
{
    using System.Linq;
    using SilverNeedle.Utility;
    using SilverNeedle.Equipment;
    using SilverNeedle.Serialization;
    public class SlowAndSteady : Trait, IComponent
    {
        public SlowAndSteady() : base() { }
        public SlowAndSteady(IObjectStore configure) : base(configure) { }
        public void Initialize(ComponentBag components)
        {
            var dwarfMove = new DwarfMovement(components);
            components.ApplyStatModifier(dwarfMove);
        }

        private class DwarfMovement : DelegateStatModifier
        {
            private Inventory inventory;
            public DwarfMovement(ComponentBag components) : base(StatNames.ArmorMovementPenalty, "trait", "Slow and Steady")
            {
                this.inventory = components.Get<Inventory>();
                this.Calculation = NegateArmorMovement;
            }

            private float NegateArmorMovement()
            {
                var armor = inventory.Equipped<Armor>();
                return -armor.Sum(x => x.MovementSpeedPenalty20);        
            }
        }
    }
}