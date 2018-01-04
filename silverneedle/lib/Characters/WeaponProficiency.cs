// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    using System.Text.RegularExpressions;
    using Inflector;
    using SilverNeedle.Equipment;
    using SilverNeedle.Serialization;

    public class WeaponProficiency
    {
        protected string[] proficiencyList;

        public WeaponProficiency(string proficiency)
        {
            this.proficiencyList = new string[] {
                proficiency
            };
        }

        public WeaponProficiency(IObjectStore configuration)
        {
            this.proficiencyList = configuration.GetList("weapons");
        }

        public string Name 
        { 
            get { return string.Join(", ", proficiencyList); }
        }

        public virtual bool IsProficient(IWeaponAttackStatistics weapon)
        {
            bool passes = false;
            foreach(var prof in proficiencyList)
            {
                WeaponTrainingLevel trainingLevel;
                if(System.Enum.TryParse<WeaponTrainingLevel>(prof, true, out trainingLevel))
                {
                    passes = weapon.Level == trainingLevel;
                }
                else if(prof.Contains("\""))
                {
                    var result = Regex.Match(prof, "[\\w\\s]+");
                    passes = weapon.ProficiencyName.ContainsIgnoreCase(result.Value);
                }
                else
                {
                    passes = weapon.ProficiencyName.EqualsIgnoreCase(prof);
                }

                if(passes)
                    return true;
            }
            return false;
        }
    }
}