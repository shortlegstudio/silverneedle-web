// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities.BloodlinePowers
{
    using SilverNeedle.Characters.Attacks;
    using SilverNeedle.Dice;
    using SilverNeedle.Utility;

    public class GraspOfTheDead : IAttack, IBloodlinePower, IComponent
    {
        public ComponentContainer Parent { get; set; }
        private ClassLevel sorcererLevels;
        private AbilityScore charisma;
        public string Name { get { return "Grasp of the Dead"; } }

        public Cup Damage
        {
            get
            {
                return new Cup(Die.GetDice(DiceSides.d6, sorcererLevels.Level));
            }
        }
        public string DamageType { get { return "slashing"; } }

        public AttackTypes AttackType { get { return AttackTypes.Special; } }

        public int SaveDC { get { return 10 + charisma.TotalModifier + sorcererLevels.Level / 2; } }

        public int Range { get { return 20; } }
        public int UsesPerDay
        {
            get
            {
                if (sorcererLevels.Level >= 20)
                    return 3;
                if(sorcererLevels.Level >= 17)
                    return 2;

                return 1;
            }
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
            this.sorcererLevels = components.Get<ClassLevel>();
            this.charisma = components.Get<AbilityScores>().GetAbility(AbilityScoreTypes.Charisma);
        }
    }
}