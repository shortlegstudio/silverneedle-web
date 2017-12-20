// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    using System.Text.RegularExpressions;
    using SilverNeedle.Equipment;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;
    public class WeaponProficiencyExoticToMartial : WeaponProficiency, IComponent
    {
        private ComponentBag components;
        public WeaponProficiencyExoticToMartial(IObjectStore configuration) : base(configuration)
        {

        }

        public override bool IsProficient(IWeaponAttackStatistics weapon)
        {
            if(weapon.Level == WeaponTrainingLevel.Exotic)
            {
                foreach(var prof in proficiencyList)
                {
                    var result = Regex.Match(prof, "[\\w\\s]+");
                    if(weapon.Name.ContainsIgnoreCase(result.Value))
                    {
                        return CheckWeaponAgainstOtherProficienciesAsIfMartial(weapon);
                    }
                }
            }
            return false;
        }

        public void Initialize(ComponentBag components)
        {
            this.components = components;
        }

        private bool CheckWeaponAgainstOtherProficienciesAsIfMartial(IWeaponAttackStatistics weapon)
        {
            var martialize = new ExoticToMartialWeaponDecorator(weapon);
            var proficiencies = components.GetAll<WeaponProficiency>().Exclude(this);

            return proficiencies.IsProficient(martialize);
        }
    }
}