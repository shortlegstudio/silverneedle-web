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

    public class MonkUnarmedStrike : IComponent, IImprovesWithLevels
    {
        public UnarmedMonk Attack { get; private set; }
        private IDictionary<int, string> damageTable = new Dictionary<int, string>();

        public MonkUnarmedStrike(IObjectStore configuration)
        {
            foreach(var c in configuration.GetObject("damage-table").Children)
            {
                damageTable.Add(int.Parse(c.Key), c.Value);
            }
        }

        public void Initialize(ComponentBag components)
        {
            var level = components.Get<ClassLevel>();
            var size = components.Get<SizeStats>().Size;
            Attack = new UnarmedMonk(DamageTables.ConvertDamageBySize(damageTable[level.Level], size));
            var offense = components.Get<OffenseStats>();
            offense.AddAttack(Attack);
        }

        public void LeveledUp(ComponentBag components)
        {
            var level = components.Get<ClassLevel>();
            var size = components.Get<SizeStats>().Size;
            Attack.Damage = Dice.DiceStrings.ParseDice(DamageTables.ConvertDamageBySize(damageTable[level.Level], size));
        }
    }
}