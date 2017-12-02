// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Attacks
{
    using System;
    using SilverNeedle.Dice;
    public class AcidDart : WeaponAttack
    {
        private ClassLevel source;
        private AbilityScore baseAbility;
        public AcidDart(ClassLevel source, AbilityScore baseAbility)
        {
            this.source = source;
            this.baseAbility = baseAbility;
            this.AttackType = AttackTypes.Ranged;
            this.DamageType = "acid";
            this.Range = 30;
        }

        public override Cup Damage
        {
            get
            {
                var cup = new Cup(Die.D6());
                cup.Modifier = Math.Max(1, source.Level / 2);
                return cup;
            }
        }

        public int UsesPerDay
        {
            get
            {
                return 3 + baseAbility.TotalModifier;
            }
        }

        public override string DisplayString()
        {
            return string.Format("Acid Dart {0}' ({1} {2})", this.Range.ToString(), this.Damage.ToString(), this.DamageType);
        }
    }
}