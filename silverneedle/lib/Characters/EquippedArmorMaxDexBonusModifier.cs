// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Characters
{
    using System.Linq;
    using SilverNeedle.Equipment;
    using SilverNeedle.Utility;
    public class EquippedArmorMaxDexBonusModifier : IValueStatModifier, IComponent
    {
        private Inventory inventory;
        public EquippedArmorMaxDexBonusModifier() { }
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

        public string ModifierType { get { return "Max Dex"; } }
        public string StatisticName { get { return StatNames.MaxDexterityBonus; } }
        public string Condition { get; set; }
        public string StatisticType { get; private set; }
        public ComponentContainer Parent { get; set; }

        public void Initialize(ComponentContainer components)
        {
            inventory = components.Get<Inventory>();
        }
    }
}