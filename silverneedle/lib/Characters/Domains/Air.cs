// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Domains
{
    using System;
    using SilverNeedle.Characters.Attacks;
    using SilverNeedle.Dice;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;
    public class Air : Domain, IComponent, IImprovesWithLevels
    {
        private ClassLevel clericLevel;
        private LightningArcAttack lightningAttack; 
        private DamageResistance electricity;
        public Air(IObjectStore data) : base(data)
        {
            
        }

        public void Initialize(ComponentBag components)
        {
            clericLevel = components.Get<ClassLevel>();
            this.lightningAttack = new LightningArcAttack(components);
            var offense = components.Get<OffenseStats>();
            offense.AddAttack(lightningAttack);
        }

        public void LeveledUp(ComponentBag components)
        {
            var defenseStats = components.Get<DefenseStats>();
            if(clericLevel.Level == 6)
            {
                electricity = new DamageResistance(10, "electricity");
                defenseStats.AddDamageResistance(electricity);
            } 

            if(clericLevel.Level == 12)
            {
                electricity.Amount = 20;
            }

            if(clericLevel.Level == 20)
            {
                electricity.Amount = 0;
                defenseStats.AddImmunity("electricity");
            }
        }
        public class LightningArcAttack : WeaponAttack
        {
            private ClassLevel clericLevel;
            private AbilityScore wisdom;
            public LightningArcAttack(ComponentBag components)
            {
                clericLevel = components.Get<ClassLevel>();
                wisdom = components.Get<AbilityScores>().GetAbility(AbilityScoreTypes.Wisdom);
                this.AttackType = AttackTypes.Ranged;
                this.DamageType = "electricity";
                this.Range = 30;
            }

            public override Cup Damage
            {
                get
                {
                    var cup = new Cup(Die.D6());
                    cup.Modifier = Math.Max(1, clericLevel.Level / 2);
                    return cup;
                }
            }

            public int UsesPerDay
            {
                get
                {
                    return 3 + wisdom.TotalModifier;
                }
            }

            public override string ToString()
            {
                return string.Format("Lightning Arc {0}' ({1} {2})", this.Range.ToString(), this.Damage.ToString(), this.DamageType);
            }
        }
    }
}