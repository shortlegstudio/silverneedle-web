// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    using System.Linq;
    using SilverNeedle.Utility;
    using SilverNeedle.Equipment;
    
    public class EquippedArmorMovementModifier : DelegateStatModifier
    {
        private Inventory inventory;
        private MovementStats movement;
        
        public EquippedArmorMovementModifier(ComponentContainer components) : base(StatNames.ArmorMovementPenalty, "Armor")
        {
            this.inventory = components.Get<Inventory>();
            this.movement = components.Get<MovementStats>();
            this.Calculation = CalculateMovementModifier;
        } 

        public float CalculateMovementModifier()
        {
            if(movement.BaseMovement.BaseValue == 30)
            {
                return inventory.Equipped<IArmor>().Sum(x => x.MovementSpeedPenalty30);
            }
            else
            {
                return inventory.Equipped<IArmor>().Sum(x => x.MovementSpeedPenalty20);
            }
        }
    }
}