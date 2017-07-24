// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Attacks
{
    using System;
    using SilverNeedle.Dice;
    //TODO: These attacks should have configuration yaml files
    public class Icicle : AttackStatistic
    {
        private ClassLevel source;
        private AbilityScore baseAbility;
        private int Range;
        public Icicle(ClassLevel source, AbilityScore baseAbility)
        {
            this.source = source;
            this.baseAbility = baseAbility;
            this.AttackType = AttackTypes.Ranged;
            this.DamageType = "cold";
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

        public override string ToString()
        {
            return string.Format("Icicle {0}' ({1} {2})", this.Range.ToString(), this.Damage.ToString(), this.DamageType);
        }
    }
}