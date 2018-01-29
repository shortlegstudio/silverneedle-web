// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Characters.Prerequisites
{
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    public class ArmorProficiencyPrerequisite : IPrerequisite
    {
        public ArmorProficiencyPrerequisite(IObjectStore config)
        {
            config.Deserialize(this);
        }

        [ObjectStore("armor")]
        public string Proficiency { get; private set; }

        [ObjectStoreOptional("not-proficient")]
        public bool CheckForNonProficiency { get; private set; }

        public bool IsQualified(ComponentContainer components)
        {
            var profs = components.GetAll<ArmorProficiency>();
            foreach(var p in profs)
            {
                if(p.IsProficient(Proficiency))
                    return !CheckForNonProficiency;
            }
            return CheckForNonProficiency;
        }
    }
}