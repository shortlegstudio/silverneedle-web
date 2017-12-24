// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Characters.SpecialAbilities.BloodlinePowers
{
    using SilverNeedle.Characters.Attacks;
    using SilverNeedle.Dice;
    using SilverNeedle.Utility; 
    public class ElementalBlast : IAttackStatistic, IBloodlinePower, IComponent
    {
        private ClassLevel sorcererLevels;
        private ElementalType elementalType;
        private AbilityScore charisma;
        public string Name { get { return "Elemental Blast"; } }

        public IStatistic AttackBonus => throw new System.NotImplementedException();

        public Cup Damage
        {
            get
            {
                return new Cup(Die.GetDice(DiceSides.d6, sorcererLevels.Level));
            }
        }
        public string DamageType { get { return elementalType.EnergyType; } }

        public int NumberOfAttacks { get { return 1; } }

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

        public int Range { get { return 20; } }
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
            throw new System.NotImplementedException();
        }

        public string DisplayString()
        {
            return "{0}/day {1} {2} radius ({3} {4}, DC {5})".Formatted(
                UsesPerDay,
                Name,
                Range.ToRangeString(),
                Damage,
                DamageType,
                SaveDC
            );
        }

        public void Initialize(ComponentContainer components)
        {
            sorcererLevels = components.Get<ClassLevel>();
            elementalType = components.Get<ElementalType>();
            charisma = components.Get<AbilityScores>().GetAbility(AbilityScoreTypes.Charisma);
        }
    }
}