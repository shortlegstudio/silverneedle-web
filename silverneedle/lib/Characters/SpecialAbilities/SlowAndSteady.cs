// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using System.Linq;
    using SilverNeedle.Utility;
    using SilverNeedle.Equipment;
    using SilverNeedle.Serialization;

    public class SlowAndSteady : DelegateStatModifier
    {
        private Inventory inventory;
        public SlowAndSteady(ComponentBag components) : base(StatNames.ArmorMovementPenalty, "trait", "Slow and Steady")
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