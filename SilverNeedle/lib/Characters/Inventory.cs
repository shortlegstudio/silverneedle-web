//-----------------------------------------------------------------------
// <copyright file="Inventory.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SilverNeedle.Characters
{
    using System.Collections.Generic;
    using System.Linq;
    using SilverNeedle.Equipment;
    using SilverNeedle.Treasure;

    /// <summary>
    /// Inventory for a character. This holds all the gear and keeps track
    /// of what they have equipped.
    /// </summary>
    public class Inventory
    {
        public event System.EventHandler<InventoryEventArgs> ItemAdded;
        /// <summary>
        /// The gear a character has
        /// </summary>
        private IList<Possession> gear;

        /// <summary>
        /// Initializes a new instance of the <see cref="SilverNeedle.Characters.Inventory"/> class.
        /// </summary>
        public Inventory()
        {
            this.gear = new List<Possession>();  
            this.CoinPurse = new CoinPurse();
        }

        public CoinPurse CoinPurse { get; private set; }

        /// <summary>
        /// Gets all of the gear
        /// </summary>
        /// <value>All of the equipment in inventory.</value>
        public IEnumerable<Possession> All 
        { 
            get { return this.gear; } 
        }

        /// <summary>
        /// Gets all of the weapons 
        /// </summary>
        /// <value>The weapons.</value>
        public IEnumerable<IWeapon> Weapons 
        { 
            get 
            { 
                return this.gear.Select(x => x.ReferenceObject).OfType<IWeapon>(); 
            } 
        }

        /// <summary>
        /// Gets all of the armor
        /// </summary>
        /// <value>The armor.</value>
        public IEnumerable<IArmor> Armor 
        { 
            get 
            { 
                
                return this.gear.Select(x => x.ReferenceObject).OfType<IArmor>(); 
            } 
        }

        public IEnumerable<Spellbook> Spellbooks
        {
            get
            {
                return this.gear.Select(x => x.ReferenceObject).OfType<Spellbook>();
            }
        }

        /// <summary>
        /// Gets the equipped items.
        /// </summary>
        /// <value>The equipped items.</value>
        public IEnumerable<Possession> EquippedItems 
        { 
            get { return this.gear.Where(x => x.IsEquipped); } 
        }

        /// <summary>
        /// Adds the gear to the character.
        /// </summary>
        /// <param name="equip">Equipment to add.</param>
        public Possession AddGear(IGear equip)
        {
            var possession = Find(equip);
            if(possession == null)
            {
                possession = new Possession(equip);
                this.gear.Add(possession);
            }
            else 
            {
                possession.IncrementQuantity();
            }

            OnItemAdded(equip);
            return possession;
        }

        /// <summary>
        /// Equips the item.
        /// </summary>
        /// <param name="item">Item to equip. Adds gear if not already added</param>
        public void EquipItem(IGear item)
        {
            var pos = this.AddGear(item);
            pos.IsEquipped = true;
        }

        public IEnumerable<T> Equipped<T>() where T : IGear
        {
            return this.gear
                .Where(x => 
                    x.IsEquipped && 
                    x.ReferenceObject is T
                ).Select(x => (T)x.ReferenceObject);
        }

        /// <summary>
        /// Finds gear of specific type
        /// </summary>
        /// <returns>The type.</returns>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public IEnumerable<T> GearOfType<T>()
        {
            return this.gear.Select(x => x.ReferenceObject).OfType<T>();
        }

        public void Purchase(IGear item)
        {
            CoinPurse.Spend(item.Value);
            AddGear(item);
        }

        public Possession Find(IGear item)
        {
            return this.gear.FirstOrDefault(x => x.ReferenceObject == item);
        }

        public string[] ToStringArray()
        {
            return this.gear.Select(
                x => x.GroupSimilar && (x.Quantity > 1) ? string.Format("{0} ({1})", x.Name, x.Quantity) : x.Name
            ).ToArray();
        }

        private void OnItemAdded(IGear item)
        {
            if(ItemAdded != null)
                ItemAdded(this, new InventoryEventArgs(item, this));
        }
    }
}