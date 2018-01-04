// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Characters
{
    using System.Linq;
    using SilverNeedle.Equipment;
    using SilverNeedle.Utility;
    public class EquippedArmorMaxDexBonuxModifier : IStatModifier
    {
        private Inventory inventory;
        public EquippedArmorMaxDexBonuxModifier(ComponentContainer components)
        {
            inventory = components.Get<Inventory>();
        }
        public float Modifier 
        {
            get
            {
                var armors = inventory.Equipped<IArmor>();
                if (armors.Count() == 0)
                    return 10000;
                return armors.Min(x => x.MaximumDexterityBonus);
            }
        }

        public string Reason { get { return "Equipped Armor"; } }

        public string ModifierType { get { return "Max Dex"; } }
        public string StatisticName { get { return StatNames.MaxDexterityBonus; } }
        public string Condition { get; set; }
        public string StatisticType { get; private set; }
    }
}