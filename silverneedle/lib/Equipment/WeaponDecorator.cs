// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Equipment
{
    public abstract class WeaponDecorator : IWeapon
    {
        private IWeapon baseWeapon;

        public virtual string Damage => baseWeapon.Damage;

        public virtual DamageTypes DamageType => baseWeapon.DamageType;

        public virtual int CriticalThreat => baseWeapon.CriticalThreat;

        public virtual int CriticalModifier => baseWeapon.CriticalModifier;

        public virtual int Range => baseWeapon.Range;

        public virtual WeaponType Type => baseWeapon.Type;

        public virtual WeaponTrainingLevel Level => baseWeapon.Level;

        public virtual string Name => baseWeapon.Name;

        public virtual float Weight => baseWeapon.Weight;

        public virtual int Value => baseWeapon.Value;

        public virtual bool GroupSimilar => baseWeapon.GroupSimilar;
        public virtual int AttackModifier => baseWeapon.AttackModifier;

        public virtual WeaponGroup Group => baseWeapon.Group;
        public virtual string ProficiencyName => baseWeapon.ProficiencyName;
        public virtual bool IsRanged => baseWeapon.IsRanged;
        public virtual bool IsMelee => baseWeapon.IsMelee;

        public WeaponDecorator(IWeapon reference)
        {
            baseWeapon = reference;
        }
    }
}