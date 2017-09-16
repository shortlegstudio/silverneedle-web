// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Characters.SpecialAbilities
{
    using SilverNeedle.Equipment;
    using SilverNeedle.Utility;
    public class MonkArmorClassBonus : SpecialAbility, IComponent
    {
        private IStatModifier wisdomACBonus;
        private AbilityScore wisdom;
        private Inventory inventory;
        public void Initialize(ComponentBag components)
        {
            wisdom = components.FindStat<AbilityScore>(StatNames.Wisdom);
            inventory = components.Get<Inventory>();
            wisdomACBonus = new DelegateStatModifier(StatNames.ArmorClass, "bonus", this.Name, Modifier);
            components.ApplyStatModifier(wisdomACBonus);
        }

        private float Modifier()
        {
            if(inventory.Equipped<Armor>().NotEmpty())
                return 0;

            return wisdom.TotalModifier;
        }
    }
}