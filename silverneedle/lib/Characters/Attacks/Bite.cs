// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Characters.Attacks
{
    using SilverNeedle.Dice;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    public class Bite : IAttack, IComponent
    {
        [ObjectStore("attack-bonus")]
        public BasicStat AttackBonus { get; private set; }

        [ObjectStore("damage")]
        public DiceStatistic DamageDice { get; private set; }

        [ObjectStore("name")]
        public string Name { get; private set; }

        [ObjectStore("medium-damage")]
        private string BaseDamage { get; set; }

        public Cup Damage { get { return DamageDice.Dice; } }

        [ObjectStore("attack-type")]
        public AttackTypes AttackType { get; private set; }

        public string DisplayString()
        {
            return "{0} {1} ({2})".Formatted(Name, AttackBonus.TotalValue.ToModifierString(), Damage);
        }

        public void Initialize(ComponentContainer components)
        {
            components.Add(this.AttackBonus);
            components.Add(this.DamageDice);
        }

        public Bite(IObjectStore configuration)
        {
            configuration.Deserialize(this);
        }
    }
}