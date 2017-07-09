// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    using SilverNeedle.Dice;
    using SilverNeedle.Equipment;
    using SilverNeedle.Utility;
    /// <summary>
    /// Attack statistics for offense
    /// </summary>
    public class AttackStatistic
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the weapon.
        /// </summary>
        /// <value>The weapon.</value>
        public virtual IWeapon Weapon { get; set; }

        /// <summary>
        /// Gets or sets the damage.
        /// </summary>
        /// <value>The dice needed to roll damage.</value>
        public virtual Cup Damage { get; set; }

        /// <summary>
        /// Gets or sets the attack bonus.
        /// </summary>
        public virtual int AttackBonus { get; set; }

        public virtual AttackTypes AttackType { get; set; }

        public virtual int CriticalModifier { get; set; }
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
                this.AttackBonus.ToModifierString(),
                this.Damage,
                this.Weapon.CriticalThreat,
                this.CriticalModifier,
                this.AttackType == AttackTypes.Ranged ? this.Weapon.Range.ToRangeString() : "");
        }
    }
}