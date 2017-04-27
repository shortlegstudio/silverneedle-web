// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using System.Linq;
    using SilverNeedle.Characters;
    using SilverNeedle.Equipment;
    using SilverNeedle.Utility;
    public class FastMovement : SpecialAbility, IComponent
    {
        public FastMovement(int speed)
        {
            this.Name = "Fast Movement";
            this.Speed = speed;
            this.MovementModifier = new DelegateStatModifier(
                StatNames.BaseMovementSpeed,
                "bonus",
                "Fast Movement",
                MovementBonus
            );
        }

        public int Speed { get; private set; }

        public void Initialize(ComponentBag components)
        {
            components.ApplyStatModifier(MovementModifier);
            inventory = components.Get<Inventory>();
        }

        private IStatModifier MovementModifier { get; set; }
        private Inventory inventory { get; set; }

        private float MovementBonus()
        {
            if(inventory.Equipped<Armor>().Any(x => x.ArmorType == ArmorType.Heavy))
                return 0;

            return Speed;
        }
    }
}