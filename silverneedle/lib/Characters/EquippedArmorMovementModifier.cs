// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    using System.Linq;
    using SilverNeedle.Utility;
    using SilverNeedle.Equipment;
    
    public class EquippedArmorMovementModifier : DelegateStatModifier, IComponent
    {
        private Inventory inventory;
        private MovementStats movement;
        
        public EquippedArmorMovementModifier() : base(StatNames.ArmorMovementPenalty, "Armor") { }

        public ComponentContainer Parent { get; set; }

        public float CalculateMovementModifier()
        {
            if(movement.UseBase30MoveSpeed)
            {
                return inventory.Equipped<IArmor>().Sum(x => x.MovementSpeedPenalty30);
            }
            else
            {
                return inventory.Equipped<IArmor>().Sum(x => x.MovementSpeedPenalty20);
            }
        }

        public void Initialize(ComponentContainer components)
        {
            this.inventory = components.Get<Inventory>();
            this.movement = components.Get<MovementStats>();
            this.Calculation = CalculateMovementModifier;
        }
    }
}