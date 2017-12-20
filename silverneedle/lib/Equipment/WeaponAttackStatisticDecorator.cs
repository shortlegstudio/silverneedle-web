// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Equipment
{
    public abstract class WeaponAttackStatisticDecorator : IWeaponAttackStatistics
    {
        private IWeaponAttackStatistics reference;
        public WeaponAttackStatisticDecorator(IWeaponAttackStatistics reference)
        {
            this.reference = reference;
        }

        public virtual string Damage => reference.Damage;

        public virtual DamageTypes DamageType => reference.DamageType;

        public virtual int CriticalThreat => reference.CriticalThreat;

        public virtual int CriticalModifier => reference.CriticalModifier;

        public virtual int Range => reference.Range;

        public virtual WeaponType Type => reference.Type;

        public virtual WeaponTrainingLevel Level => reference.Level;

        public virtual int AttackModifier => reference.AttackModifier;

        public virtual WeaponGroup Group => reference.Group;

        public virtual string ProficiencyName => reference.ProficiencyName;

        public virtual bool IsRanged => reference.IsRanged;

        public virtual bool IsMelee => reference.IsMelee;

        public virtual string Name => reference.Name;
    }

}