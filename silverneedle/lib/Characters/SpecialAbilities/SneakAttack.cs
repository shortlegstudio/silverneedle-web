// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using SilverNeedle.Characters.Attacks;
    using SilverNeedle.Dice;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    public class SneakAttack : IAbility, IComponent, INameByType
    {
        public IDiceStatistic Damage { get; private set; }
        public SneakAttackAttackStatistic Attack { get; private set; }

        public SneakAttack(IObjectStore configuration)
        {
            this.Damage = new DiceStatistic(configuration.GetObject("dice-stat"));
            this.Attack = new SneakAttackAttackStatistic(this.Damage);
        }

        public string DisplayString()
        {
            return "{0} ({1})".Formatted(this.Name(), this.Damage.Dice);
        }

        public void Initialize(ComponentContainer components)
        {
            components.Add(this.Damage);
            components.Add(this.Attack);
        }

        public class SneakAttackAttackStatistic : IAttack
        {
            private IDiceStatistic sneakAttackDice;
            public SneakAttackAttackStatistic(IDiceStatistic sneakAttackDice)
            {
                this.sneakAttackDice = sneakAttackDice;
            }
            public Cup Damage 
            {
                get { return sneakAttackDice.Dice; }
            }

            public string Name { get { return "Sneak Attack"; } }

            public AttackTypes AttackType { get { return AttackTypes.Special; } }

            public string DisplayString()
            {
                return string.Format("{0} {1}", this.Name, this.Damage);
            }
        }
    }
}