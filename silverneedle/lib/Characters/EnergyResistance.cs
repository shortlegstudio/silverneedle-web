// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    using System;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Core;
    public class EnergyResistance : IResistance
    {
        public const int IMMUNITY_THRESHOLD = 10000;
        public EnergyResistance(int amnt, string damageType) 
        {
            this.DamageType = damageType;
            this.amount = new BasicStat(this.Name, amnt);
        }

        public EnergyResistance(IDamageType damageType, Func<float> calculation)
        {
            DamageType = damageType.Name;
            this.amount = new BasicStat(this.Name);
            this.amount.AddModifier(
                new DelegateStatModifier(this.Name, "-", "-", calculation)
            );
        }

        private BasicStat amount;

        public string Name { get { return "Energy Resistance ({0})".Formatted(DamageType); } }
        public string DamageType { get; private set; }
        public int Amount 
        { 
            get { return this.amount.TotalValue; }
            set { this.amount.SetValue(value); }
        }

        public bool IsImmune
        {
            get { return Amount >= IMMUNITY_THRESHOLD; }
        }

        public void SetToImmunity()
        {
            Amount = IMMUNITY_THRESHOLD;
        }

        public static EnergyResistance CreateImmunity(string damageType)
        {
            return new EnergyResistance(IMMUNITY_THRESHOLD, damageType);
        }

        public string DisplayString()
        {
            return "{0} {1}".Formatted(this.DamageType, this.Amount);
        }
    }
}