// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using SilverNeedle.Utility;
    using SilverNeedle.Serialization;
    using SilverNeedle.Bestiary;
    using SilverNeedle.Equipment;

    public class ArcaneBond : INameByType, IAbility, BloodlinePowers.IBloodlinePower, IComponent
    {
        public ComponentContainer Parent { get; set; }
        private EntityGateway<Familiar> familiars;
        private IGear[] items;
        private Familiar bondedFamiliar;
        private IGear bondedItem;
        public bool IsFamiliarBond { get { return bondedFamiliar != null; } }
        public bool IsItemBond { get { return bondedItem != null; } }
        public ArcaneBond() : this(GatewayProvider.Get<Familiar>())
        {

        }
        public ArcaneBond(EntityGateway<Familiar> familiars)
        {
            this.familiars = familiars;
            items = new IGear[]
            {
                new Gear("amulet", 0, 0),
                new Gear("ring", 0, 0),
                new Gear("staff", 0, 0),
                new Gear("wand", 0, 0),
                new Gear("ring", 0, 0)
            };
        }
        public void Initialize(ComponentContainer components)
        {
            if(Randomly.TrueFalse())
            {
                bondedFamiliar = this.familiars.ChooseOne();
                components.Add(bondedFamiliar);
            }
            else
            {
                bondedItem = this.items.ChooseOne();
                components.Get<Inventory>().EquipItem(bondedItem);
            }
        }

        public string DisplayString() 
        { 
            if(IsFamiliarBond)
                return "{0} ({1})".Formatted(this.Name(), bondedFamiliar.Name);
            
            if(IsItemBond)
                return "{0} ({1})".Formatted(this.Name(), bondedItem.Name);

            return this.Name();
        }
    }
}