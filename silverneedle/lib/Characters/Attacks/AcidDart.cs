// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Attacks
{
    using System;
    using SilverNeedle.Dice;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    public class AcidDart : WeaponAttack, IComponent
    {
        private ClassLevel source;
        private AbilityScore baseAbility;
        private AbilityScoreTypes baseAbilityType;
        
        private AcidDart()
        {
            this.AttackType = AttackTypes.Ranged;
            this.DamageType = "acid";
            this.Range = 30;
        }
        public AcidDart(IObjectStore configuration) : this()
        {
            baseAbilityType = configuration.GetEnum<AbilityScoreTypes>("base-ability");
        }
        public AcidDart(ClassLevel source, AbilityScore baseAbility) : this()
        {
            this.source = source;
            this.baseAbility = baseAbility;
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

        public void Initialize(ComponentContainer components)
        {
            if(baseAbility == null)
            {
                source = components.Get<ClassLevel>();
                baseAbility = components.Get<AbilityScores>().GetAbility(baseAbilityType);
            }
        }
    }
}