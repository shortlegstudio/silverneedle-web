// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities.BloodlinePowers
{
    using SilverNeedle.Characters.Attacks;
    using SilverNeedle.Dice;
    using SilverNeedle.Utility;

    public class BreathWeapon : IBloodlinePower, IAttackStatistic, IComponent
    {
        private ClassLevel sorcererLevels;
        private DragonType dragonType;
        private AbilityScore charisma;
        public string Name { get { return "Breath Weapon"; } }

        public IStatistic AttackBonus => throw new System.NotImplementedException();

        public Cup Damage 
        { 
            get 
            { 
                return new Cup(Die.GetDice(DiceSides.d6, sorcererLevels.Level));
            }
        }

        public int NumberOfAttacks => throw new System.NotImplementedException();

        public AttackTypes AttackType { get { return AttackTypes.Special; } }

        public IStatistic CriticalModifier => throw new System.NotImplementedException();

        public int CriticalThreat => throw new System.NotImplementedException();

        public int SaveDC 
        {
            get
            {
                return 10 + charisma.TotalModifier + sorcererLevels.Level / 2;
            }
        }

        public int Range { get { return dragonType.BreathRange; } }

        public string DamageType { get { return dragonType.EnergyType; } }
        public string Shape { get { return dragonType.BreathShape; } }
        public int UsesPerDay 
        {
            get
            {
                if(sorcererLevels.Level >= 20)
                    return 3;
                if(sorcererLevels.Level >= 17)
                    return 2;
                
                return 1;
            }
        }

        public string AttackBonusString()
        {
            return string.Empty;
        }

        public string DisplayString()
        {
            return "{0} ({1} {2}, {3} {4}, DC {5}, {6}/day)".Formatted(
                this.Name,
                this.Range.ToRangeString(),
                this.Shape,
                this.Damage,
                this.DamageType,
                this.SaveDC,
                this.UsesPerDay
            );
        }

        public void Initialize(ComponentBag components)
        {
            this.sorcererLevels = components.Get<ClassLevel>();
            var bloodline = components.Get<IDraconicBloodline>();
            this.dragonType = bloodline.DragonType;
            this.charisma = components.Get<AbilityScores>().GetAbility(AbilityScoreTypes.Charisma);
        }
    }
}