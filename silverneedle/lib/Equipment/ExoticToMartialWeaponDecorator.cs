// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Equipment
{
    public class ExoticToMartialWeaponDecorator : WeaponAttackStatisticDecorator
    {
        public ExoticToMartialWeaponDecorator(IWeaponAttackStatistics reference) : base(reference)
        {
        }

        public override WeaponTrainingLevel Level 
        {
            get 
            { 
                return base.Level == WeaponTrainingLevel.Exotic ? WeaponTrainingLevel.Martial : base.Level;
            }
        }
    }
}