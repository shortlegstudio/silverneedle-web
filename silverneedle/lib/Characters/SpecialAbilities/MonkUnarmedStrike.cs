// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Characters.SpecialAbilities
{
    using System;
    using System.Collections.Generic;
    using SilverNeedle.Characters.Attacks;
    using SilverNeedle.Utility;
    using SilverNeedle.Serialization;
    using SilverNeedle.Equipment;

    public class MonkUnarmedStrike : IComponent
    {
        public UnarmedMonk Attack { get; private set; }
        private ClassLevel monkLevels;
        private DataTable damageTable;
        private CharacterSize size;

        public MonkUnarmedStrike()
        {
            damageTable = GatewayProvider.Find<DataTable>("Monk Abilities");
        }

        public MonkUnarmedStrike(DataTable damageTable)
        {
            this.damageTable = damageTable;
        }

        public void Initialize(ComponentBag components)
        {
            monkLevels = components.Get<ClassLevel>();
            size = components.Get<SizeStats>().Size;
            Attack = new UnarmedMonk(this);
            var offense = components.Get<OffenseStats>();
            offense.AddAttack(Attack);
        }

        public string GetDamage()
        {
            return DamageTables.ConvertDamageBySize(
                damageTable.Get(monkLevels.Level.ToString(), "unarmed-damage"), 
                size
            );
        }
    }
}