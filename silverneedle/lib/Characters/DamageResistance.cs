// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    using System;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Core;
    public class DamageResistance : SpecialAbility
    {
        public const int IMMUNITY_THRESHOLD = 10000;
        public DamageResistance(int amnt, string damageType) 
        {
            this.DamageType = damageType;
            this.Name = StatNames.DamageResistance + " " + damageType;
            this.amount = new BasicStat(this.Name, amnt);
        }

        public DamageResistance(IDamageType damageType, Func<float> calculation)
        {
            DamageType = damageType.Name;
            this.Name = StatNames.DamageResistance + " " + damageType;
            this.amount = new BasicStat(this.Name);
            this.amount.AddModifier(
                new DelegateStatModifier(this.Name, "-", "-", calculation)
            );
        }

        private BasicStat amount;

        public string DamageType { get; private set; }
        public int Amount 
        { 
            get { return this.amount.TotalValue; }
            set { this.amount.SetValue(value); }
        }

        public bool IsImmune()
        {
            return Amount >= IMMUNITY_THRESHOLD;
        }

        public override string ToString() 
        {
            return string.Format("{0}/{1}", this.Amount, this.DamageType);
        }

        public void SetToImmunity()
        {
            Amount = IMMUNITY_THRESHOLD;
        }

        public static DamageResistance CreateImmunity(string damageType)
        {
            return new DamageResistance(IMMUNITY_THRESHOLD, damageType);
        }
    }
}