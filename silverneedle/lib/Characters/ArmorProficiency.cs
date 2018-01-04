// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    using SilverNeedle;
    using SilverNeedle.Equipment;
    using SilverNeedle.Serialization;

    public class ArmorProficiency
    {
        public ArmorProficiency(string proficiency)
        {
            this.ProficiencyList = new string[] { proficiency };
        }

        public ArmorProficiency(IObjectStore configuration)
        {
            configuration.Deserialize(this);
        }

        public string Name 
        {
            get { return string.Join(", ", ProficiencyList); }
        }

        [ObjectStore("armors")]
        public string[] ProficiencyList { get; private set; }

        public bool IsProficient(IArmor armor)
        {
            bool passes = false;
            foreach(var prof in ProficiencyList)
            {
                ArmorType armorType;
                if(System.Enum.TryParse<ArmorType>(prof, true, out armorType))
                {
                    passes = armor.ArmorType == armorType;
                }
                else
                {
                    passes = armor.Name.EqualsIgnoreCase(prof);
                }

                //Exit out if any pass
                if(passes)
                    return passes;
            }

            //Nothing should pass, force to false
            return false;
        }
    }
}