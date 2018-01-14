// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Characters
{
    using System.Linq;
    using SilverNeedle.Utility;
    using SilverNeedle.Equipment;

    public class EquippedArmorClassModifier : IValueStatModifier
    {
        private Inventory inventory;

        public EquippedArmorClassModifier(ComponentContainer components)
        {
            this.inventory = components.Get<Inventory>();            
        }

        public float Modifier 
        {
            get
            {
                var armor = inventory.Equipped<IArmor>();
                if (armor.Count() == 0)
                    return 0;

                return armor.First().ArmorClass;
                
            }
        }

        public string Reason { get { return "Equipped Armor"; } }

        public string ModifierType { get { return "Armor"; } }

        public string StatisticName { get { return StatNames.ArmorClass; } }
        public string Condition { get; set; }
        public string StatisticType { get; private set; }
    }
}