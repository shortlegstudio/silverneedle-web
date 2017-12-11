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

    public class ArcaneBond : SpecialAbility, BloodlinePowers.IBloodlinePower, IComponent
    {
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
        public void Initialize(ComponentBag components)
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

        public override string Name 
        { 
            get 
            { 
                if(IsFamiliarBond)
                    return "{0} ({1})".Formatted(base.Name, bondedFamiliar.Name);
                
                if(IsItemBond)
                    return "{0} ({1})".Formatted(base.Name, bondedItem.Name);

                return base.Name;
            }
        }
    }
}