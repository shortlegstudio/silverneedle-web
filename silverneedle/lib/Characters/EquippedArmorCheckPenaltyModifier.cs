// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    using System.Linq;
    using SilverNeedle.Equipment;
    using SilverNeedle.Utility;
    public class EquippedArmorCheckPenaltyModifier : DelegateStatModifier
    {
        private Inventory inventory;

        public EquippedArmorCheckPenaltyModifier(ComponentContainer bag) :
            base(StatNames.ArmorCheckPenalty, "Armor", "Armor")
        {
            this.Calculation = ArmorCheckPenalty;
            this.inventory = bag.Get<Inventory>();
        }

        public float ArmorCheckPenalty()
        {
            return inventory.Equipped<IArmor>().Sum(x => x.ArmorCheckPenalty);
        }
    }
}