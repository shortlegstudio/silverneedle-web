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
        public UnarmedMonk Weapon { get; private set; }
        public MeleeAttack Attack { get; private set; }
        private ClassLevel monkLevels;
        private DataTable damageTable;

        public MonkUnarmedStrike()
        {
            damageTable = GatewayProvider.Find<DataTable>("Monk Abilities");
        }

        public MonkUnarmedStrike(DataTable damageTable)
        {
            this.damageTable = damageTable;
        }

        public void Initialize(ComponentContainer components)
        {
            monkLevels = components.Get<ClassLevel>();
            Weapon = new UnarmedMonk(this);
            var offense = components.Get<OffenseStats>();
            var strength = components.Get<AbilityScores>().GetAbility(AbilityScoreTypes.Strength);
            var size = components.Get<SizeStats>().Size;
            Attack = new MeleeAttack(offense, strength, size, Weapon);
            components.Add(Attack);
        }

        public string GetDamage()
        {
            return damageTable.Get(monkLevels.Level.ToString(), "unarmed-damage");
        }
    }
}