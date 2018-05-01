// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Characters
{
    using System.Linq;
    using SilverNeedle.Utility;
    using SilverNeedle.Equipment;
    using SilverNeedle.Serialization;

    [ObjectStoreSerializable]
    public class EquippedArmorClassModifier : IValueStatModifier, IComponent
    {
        private Inventory inventory;

        public EquippedArmorClassModifier() { }
        public EquippedArmorClassModifier(IObjectStore configuration)
        {
            configuration.Deserialize(this);
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

        public string ModifierType { get { return "Armor"; } }

        [ObjectStore("name")]
        public string StatisticName { get; set; }
        public string Condition { get; set; }
        public string StatisticType { get; private set; }
        public ComponentContainer Parent { get; set; }

        public void Initialize(ComponentContainer components)
        {
            inventory = components.Get<Inventory>();
        }
    }
}