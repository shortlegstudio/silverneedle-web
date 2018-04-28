// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using System;
    using System.Linq;
    using SilverNeedle.Characters.Attacks;
    using SilverNeedle.Equipment;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    public class WeaponTraining : CapabilityStatistic, IComponent
    {
        public ComponentContainer Parent { get; set; }
        public WeaponTraining(IObjectStore configuration) : base(configuration)
        {
            WeaponAttackBonus = new WeaponAttackModifier(
                () => { return this.TotalValue; } ,
                QualifyCheck
            );
            WeaponDamageBonus = new WeaponDamageModifier(
                () => { return this.TotalValue; },
                QualifyCheck
            );
        }

        private bool QualifyCheck(IWeaponAttackStatistics weapon)
        {
            return weapon.Group == this.Group;
        }
        public WeaponGroup Group { get; private set; }

        public WeaponAttackModifier WeaponAttackBonus { get; private set; }
        public WeaponDamageModifier WeaponDamageBonus { get; private set; }
        public void Initialize(ComponentContainer components)
        {
            var trainings = components.GetAll<WeaponTraining>().Exclude(this);
            this.Group = EnumHelpers.GetValues<WeaponGroup>().Where(grp => !trainings.Any(already => already.Group == grp)).ChooseOne();
            var offStats = components.Get<OffenseStats>();
            offStats.AddWeaponModifier(WeaponAttackBonus);
            offStats.AddWeaponModifier(WeaponDamageBonus);
        }

        public override string DisplayString()
        {
            return string.Format("Weapon Training ({0} +{1})", this.Group, this.TotalValue);
        }
    }
}