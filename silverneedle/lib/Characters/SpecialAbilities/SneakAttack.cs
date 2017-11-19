// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using SilverNeedle.Characters.Attacks;
    using SilverNeedle.Dice;
    using SilverNeedle.Utility;
    public class SneakAttack : SpecialAbility, IComponent
    {
        public string Damage { get; private set; }
        public SneakAttackAttackStatistic Attack { get; private set; }

        public SneakAttack()
        {
            this.Damage = "1d6";
            this.Attack = new SneakAttackAttackStatistic(this);
        }

        public void SetDamage(string damage)
        {
            this.Damage = damage;
        }

        public override string Name 
        {
            get
            {
                return string.Format("Sneak Attack +({0})", this.Damage);
            }
        }

        public void Initialize(ComponentBag components)
        {
            components.Add(this.Attack);
        }

        public class SneakAttackAttackStatistic : WeaponAttack
        {
            private SneakAttack sneakAttack;
            public SneakAttackAttackStatistic(SneakAttack sneakAttack)
            {
                this.sneakAttack = sneakAttack;
                this.Name = "Sneak Attack";
                this.AttackType = AttackTypes.Special;
            }
            public override Cup Damage 
            {
                get { return DiceStrings.ParseDice(sneakAttack.Damage); }
            }

            public override string ToString()
            {
                return string.Format("{0} {1}", this.Name, this.Damage);
            }
        }
    }
}