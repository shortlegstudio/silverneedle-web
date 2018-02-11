// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    using System;
    using System.Collections.Generic;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Core;
    using SilverNeedle.Serialization;

    public class EnergyResistance : IResistance, IValueStatistic
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
                new DelegateStatModifier(this.Name, "-", calculation)
            );
        }

        public EnergyResistance(IObjectStore configuration)
        {
            DamageType = configuration.GetString("damage-type");
            this.amount = new BasicStat(this.Name, configuration.GetInteger("base-value"));
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

        public int TotalValue { get { return this.Amount; } }

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

        public int GetConditionalValue(string condition)
        {
            return amount.GetConditionalValue(condition);
        }

        public IEnumerable<string> GetConditions()
        {
            return amount.GetConditions();
        }

        public bool Matches(string name)
        {
            return this.Name.EqualsIgnoreCase(name);
        }

        public IEnumerable<IStatisticModifier> Modifiers { get { return this.amount.Modifiers; } }

        public void AddModifier(IStatisticModifier modifier)
        {
            amount.AddModifier(modifier);
        }

        public void RemoveModifier(IStatisticModifier modifier)
        {
            amount.RemoveModifier(modifier);
        }
    }
}