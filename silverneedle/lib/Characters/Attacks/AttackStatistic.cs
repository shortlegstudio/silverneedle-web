// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Attacks
{
    using SilverNeedle.Dice;
    using SilverNeedle.Equipment;
    using SilverNeedle.Utility;
    /// <summary>
    /// Attack statistics for offense
    /// </summary>
    public class AttackStatistic : IAttackStatistic
    {
        protected OffenseStats offenseAbilities;
        protected CharacterSize size;
        protected Cup damageDice;
        public AttackStatistic()
        {

        }
        public AttackStatistic(OffenseStats offense, CharacterSize size, IWeapon weapon)
        {
            this.offenseAbilities = offense;
            this.size = size;
            this.Weapon = weapon;
            this.Name = weapon.Name;
            this.DamageType = weapon.DamageType.ToString();
            this.CriticalModifier = weapon.CriticalModifier;
            this.CriticalThreat = weapon.CriticalThreat;
            this.AttackBonus = new BasicStat(string.Format("{0} Attack Bonus", weapon.Name), weapon.AttackModifier);
            this.AttackBonus.AddModifier(new WeaponProficiencyAttackModifier(this.offenseAbilities, this.Weapon));

            this.DamageModifier = new BasicStat(string.Format("{0} Damage Modifier", weapon.Name), 0);
            this.damageDice = DiceStrings.ParseDice(
                DamageTables.ConvertDamageBySize(this.Weapon.Damage, size)
            );
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
        public virtual IWeapon Weapon { get; private set; }

        /// <summary>
        /// Gets or sets the damage.
        /// </summary>
        /// <value>The dice needed to roll damage.</value>
        public virtual Cup Damage 
        { 
            get 
            { 
                var copy =  damageDice.Copy();
                copy.Modifier += DamageModifier.TotalValue;
                return copy;
            }
        }

        /// <summary>
        /// Gets or sets the attack bonus.
        /// </summary>
        public virtual BasicStat AttackBonus { get; private set; }
        public virtual BasicStat DamageModifier { get; private set; }

        public virtual AttackTypes AttackType { get; set; }

        public virtual int CriticalModifier { get; set; }
        public virtual int CriticalThreat { get; set; }
        public virtual int SaveDC { get; set; }

        public string DamageType { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents the current <see cref="SilverNeedle.Characters.OffenseStats+AttackStatistic"/>.
        /// </summary>
        /// <returns>A <see cref="System.String"/> that represents the current <see cref="SilverNeedle.Characters.OffenseStats+AttackStatistic"/>.</returns>
        public override string ToString()
        {
            
            return string.Format(
                "{0} {1} ({2} / {3}x{4}) {5}",
                this.Name,
                this.AttackBonus.TotalValue.ToModifierString(),
                this.Damage,
                this.CriticalThreat,
                this.CriticalModifier,
                this.AttackType == AttackTypes.Ranged ? this.Weapon.Range.ToRangeString() : "");
        }
    }
}