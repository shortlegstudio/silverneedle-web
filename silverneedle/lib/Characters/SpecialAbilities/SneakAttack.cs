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
            this.Attack = new SneakAttackAttackStatistic();
            this.Attack.Damage = DiceStrings.ParseDice(this.Damage);
        }

        public void SetDamage(string damage)
        {
            this.Damage = damage;
            this.Attack.Damage = DiceStrings.ParseDice(this.Damage);
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
            var offense = components.Get<OffenseStats>();
            offense.AddAttack(this.Attack);
        }

        public class SneakAttackAttackStatistic : AttackStatistic
        {
            public SneakAttackAttackStatistic()
            {
                this.Name = "Sneak Attack";
                this.AttackType = AttackTypes.Special;
            }
            public override string ToString()
            {
                return string.Format("{0} {1}", this.Name, this.Damage);
            }
        }
    }
}