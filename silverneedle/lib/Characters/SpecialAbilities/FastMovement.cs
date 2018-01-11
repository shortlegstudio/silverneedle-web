// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using System.Linq;
    using SilverNeedle.Characters;
    using SilverNeedle.Equipment;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;
    public class FastMovement : IStatModifier, IAbility, IComponent
    {
        public FastMovement(IObjectStore configuration)
        {
            configuration.Deserialize(this);
        }

        [ObjectStore("modifier")]
        public int Speed { get; private set; }

        public void Initialize(ComponentContainer components)
        {
            inventory = components.Get<Inventory>();
        }

        private IStatModifier MovementModifier { get; set; }
        private Inventory inventory { get; set; }

        public float Modifier { get { return MovementBonus(); } }


        [ObjectStoreOptional("reason")]
        public string Reason { get; private set; }

        [ObjectStore("modifier-type")]
        public string ModifierType { get; private set; }

        [ObjectStore("name")]
        public string StatisticName { get; private set; }

        [ObjectStoreOptional("condition")]
        public string Condition { get; private set; }

        [ObjectStoreOptional("stat-type")]
        public string StatisticType { get; private set; }

        private float MovementBonus()
        {
            if(inventory.Equipped<Armor>().Any(x => x.ArmorType == ArmorType.Heavy))
                return 0;

            return Speed;
        }

        public string DisplayString()
        {
            return "Fast Movement ({0})".Formatted(Modifier.ToRangeString());
        }
    }
}