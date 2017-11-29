// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities.BloodlinePowers
{
    using SilverNeedle.Characters.Attacks;
    using SilverNeedle.Dice;
    using SilverNeedle.Utility;

    public class ElementalRay : IAttackStatistic, IBloodlinePower, IComponent
    {
        private ClassLevel sorcererLevels;
        private ElementalType elementalType;
        private AbilityScore charisma;
        public string Name { get { return "Elemental Ray"; } }

        public IStatistic AttackBonus { get; private set; }

        public Cup Damage 
        { 
            get
            {
                var cup = new Cup(Die.D6());
                cup.Modifier = sorcererLevels.Level / 2;
                return cup;
            }
        }

        public int NumberOfAttacks { get { return 1; } }

        public AttackTypes AttackType { get { return AttackTypes.Special; } }

        public IStatistic CriticalModifier { get; private set; }

        public int CriticalThreat { get { return 20; } }

        public int SaveDC { get { return 0; } }

        public int Range { get { return 30; } }
        public string DamageType { get { return elementalType.EnergyType; } }
        public int UsesPerDay
        {
            get
            {
                return 3 + charisma.TotalModifier;
            }
        }

        public string AttackBonusString()
        {
            return AttackBonus.TotalValue.ToModifierString();
        }

        public string DisplayString()
        {
            return "{0}/day {1} {2} ({3} {4}) {5}".Formatted(
                this.UsesPerDay,
                this.Name,
                this.AttackBonusString(),
                this.Damage,
                this.DamageType,
                this.Range.ToRangeString()
            );
        }

        public void Initialize(ComponentBag components)
        {
            sorcererLevels = components.Get<ClassLevel>();
            CriticalModifier = new BasicStat("Elemental Ray Critical Modifier", 2);
            elementalType = components.Get<ElementalType>();
            charisma = components.Get<AbilityScores>().GetAbility(AbilityScoreTypes.Charisma);
            AttackBonus = components.Get<OffenseStats>().RangeAttackBonus;
        }
    }
}