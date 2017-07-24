// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Equipment
{
    using SilverNeedle.Dice;
    using SilverNeedle.Treasure;
    using SilverNeedle.Utility;
    public class MagicWeapon : WeaponDecorator
    {
        public MagicWeapon(IWeapon weapon, int magic) : base(weapon)
        {
            this.MagicBonus = magic;
            this.MagicValue = GatewayProvider.Find<MagicWeaponValue>(magic.ToString());
        }

        public int MagicBonus { get; private set; }

        public override int AttackModifier
        {
            get { return base.AttackModifier + this.MagicBonus; }
        }

        public override string Damage 
        {
            get
            {
                var cup = DiceStrings.ParseDice(base.Damage);
                cup.Modifier += MagicBonus;
                return cup.ToString();
            }
        }

        public override string Name
        {
            get
            {
                return string.Format("{0} {1}", base.Name, MagicBonus.ToModifierString());
            }
        }

        public override int Value
        {
            get
            {
                return base.Value + this.MagicValue.Value.ToCoinValue();
            }
        }

        public MagicWeaponValue MagicValue { get; set; }
    }
}