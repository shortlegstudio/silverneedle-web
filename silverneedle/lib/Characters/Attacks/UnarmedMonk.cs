// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Attacks
{
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Equipment;
    using SilverNeedle.Dice;
    public class UnarmedMonk : IWeaponAttackStatistics
    {
        private MonkUnarmedStrike unarmedStrike;
        public UnarmedMonk(MonkUnarmedStrike unarmed)
        {
            this.unarmedStrike = unarmed;
        }

        public string Name 
        {
            get { return "Unarmed"; }
        }

        public DamageTypes DamageType { get { return DamageTypes.Bludgeoning; } }

        public int CriticalThreat { get { return 20; } }

        public int CriticalModifier { get { return 2; } }

        public int Range { get { return 0; } }

        public WeaponType Type { get { return WeaponType.Unarmed; } }

        public WeaponTrainingLevel Level { get { return WeaponTrainingLevel.Simple; } }

        public int AttackModifier { get { return 0; } }

        public WeaponGroup Group { get { return WeaponGroup.Natural; } }

        public string ProficiencyName { get { return "Unarmed"; } }

        public bool IsRanged { get { return false; } }

        public bool IsMelee { get { return true; } }

        string IWeaponAttackStatistics.Damage { get { return unarmedStrike.GetDamage(); } }

    }
}