// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Prerequisites
{
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    public class WeaponProficiencyPrerequisite : IPrerequisite
    {
        public WeaponProficiencyPrerequisite(IObjectStore configuration)
        {
            configuration.Deserialize(this);
        }

        [ObjectStore("weapon")]
        public string Proficiency { get; private set; }

        [ObjectStoreOptional("not-proficient")]
        public bool CheckForNonProficiency { get; private set; }

        public bool IsQualified(ComponentContainer components)
        {
            var proficiencies = components.GetAll<WeaponProficiency>();

            foreach(var prof in proficiencies)
            {
                if(prof.IsProficient(Proficiency))
                    return !CheckForNonProficiency;
            }
            return CheckForNonProficiency;
        }
    }

}