// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Characters.SpecialAbilities
{
    using SilverNeedle.Equipment;
    using SilverNeedle.Utility;
    using SilverNeedle.Serialization;
    public class MonkArmorClassBonus : SpecialAbility, IComponent
    {
        private IStatModifier monkACModifier;
        private AbilityScore wisdom;
        private Inventory inventory;
        private DataTable monkAbilities;
        private ClassLevel monkLevels;
        public MonkArmorClassBonus()
        {
            monkAbilities = GatewayProvider.Find<DataTable>("Monk Abilities");
        }
        public MonkArmorClassBonus(DataTable monkAbilities)
        {
            this.monkAbilities = monkAbilities;
        }
        public void Initialize(ComponentContainer components)
        {
            monkLevels = components.Get<ClassLevel>();
            wisdom = components.FindStat<AbilityScore>(StatNames.Wisdom);
            inventory = components.Get<Inventory>();
            monkACModifier = new DelegateStatModifier(StatNames.ArmorClass, "bonus", this.Name, Modifier);
            components.ApplyStatModifier(monkACModifier);

        }

        private float Modifier()
        {
            if(inventory.Equipped<Armor>().NotEmpty())
                return 0;

            return wisdom.TotalModifier + monkAbilities.Get(monkLevels.Level.ToString(), "ac-bonus").ToInteger();
        }
    }
}