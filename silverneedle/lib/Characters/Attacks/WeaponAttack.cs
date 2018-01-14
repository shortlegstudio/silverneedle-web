// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Attacks
{
    using System.Collections.Generic;
    using SilverNeedle.Dice;
    using SilverNeedle.Equipment;
    using SilverNeedle.Utility;
    /// <summary>
    /// Attack statistics for offense
    /// </summary>
    public class WeaponAttack : IAttackStatistic
    {
        private BasicStat attackBonus;
        protected OffenseStats offenseAbilities;
        protected CharacterSize size;
        public WeaponAttack()
        {
            this.attackBonus = new BasicStat(string.Format("Attack Bonus"), 0);
        }
        public WeaponAttack(OffenseStats offense, CharacterSize size, IWeaponAttackStatistics weapon)
        {
            this.offenseAbilities = offense;
            this.size = size;
            this.Weapon = weapon;
            this.Name = weapon.Name;
            this.Range = weapon.Range;
            this.DamageType = weapon.DamageType.ToString();
            this.CriticalThreat = weapon.CriticalThreat;

            this.CriticalModifier = new BasicStat(string.Format("{0} Critical Modifier", weapon.Name), weapon.CriticalModifier);

            this.attackBonus = new BasicStat(string.Format("{0} Attack Bonus", weapon.Name), weapon.AttackModifier);
            this.attackBonus.AddModifier(new WeaponProficiencyAttackModifier(this.offenseAbilities, this.Weapon));
            this.attackBonus.AddModifiers(MultipleAttackBonusModifier.GetConditionalMultipleAttackModifiers());

            this.DamageModifier = new BasicStat(string.Format("{0} Damage Modifier", weapon.Name), 0);
            foreach(var weaponModifier in offense.WeaponModifiers)
            {
                if(weaponModifier.WeaponQualifies(weapon))
                {
                    weaponModifier.ApplyModifier(this);
                }
            }
        }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the weapon.
        /// </summary>
        /// <value>The weapon.</value>
        public virtual IWeaponAttackStatistics Weapon { get; private set; }

        /// <summary>
        /// Gets or sets the damage.
        /// </summary>
        /// <value>The dice needed to roll damage.</value>
        public virtual Cup Damage 
        { 
            get 
            { 
                var damageDice = DiceStrings.ParseDice(
                    DamageTables.ConvertDamageBySize(this.Weapon.Damage, this.size)
                );
                damageDice.Modifier += DamageModifier.TotalValue;
                return damageDice;
            }
        }

        /// <summary>
        /// Gets or sets the attack bonus.
        /// </summary>
        public virtual IValueStatistic AttackBonus { get { return this.attackBonus; } }
        public virtual IValueStatistic DamageModifier { get; private set; }
        public virtual AttackTypes AttackType { get; protected set; }
        public virtual IValueStatistic CriticalModifier { get; private set; }
        public virtual int CriticalThreat { get; protected set; }
        public virtual int SaveDC { get; protected set; }
        public string DamageType { get; protected set; }
        public virtual int NumberOfAttacks 
        {
            get 
            { 
                if(this.offenseAbilities == null)
                    return 1;
                return this.offenseAbilities.BaseAttackBonus.NumberOfAttacks; 
            }
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents the current <see cref="SilverNeedle.Characters.OffenseStats+AttackStatistic"/>.
        /// </summary>
        /// <returns>A <see cref="System.String"/> that represents the current <see cref="SilverNeedle.Characters.OffenseStats+AttackStatistic"/>.</returns>
        public virtual string DisplayString()
        {
            
            return string.Format(
                "{0} {1} ({2} / {3}x{4}) {5}",
                this.Name,
                this.AttackBonusString(),
                this.Damage,
                this.CriticalThreat,
                this.CriticalModifier.TotalValue,
                this.AttackType == AttackTypes.Ranged ? this.Weapon.Range.ToRangeString() : "");
        }

        public int Range  { get; protected set; }

        public string AttackBonusString()
        {
            List<string> bonuses = new List<string>();
            for(int i = 1; i <= NumberOfAttacks; i++)
            {
                bonuses.Add(
                    this.AttackBonus.GetConditionalValue("attack {0}".Formatted(i)).ToModifierString()
                );
            }
            return string.Join("/", bonuses);
        }
    }
}