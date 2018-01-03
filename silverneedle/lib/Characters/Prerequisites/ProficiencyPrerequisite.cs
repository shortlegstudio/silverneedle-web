// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Prerequisites
{
    using SilverNeedle.Utility;

    public class ProficiencyPrerequisite : IPrerequisite
    {
        public ProficiencyPrerequisite(string value)
        {
            this.Proficiency = value;
        }

        public string Proficiency { get; set; }

        public bool IsQualified(ComponentContainer components)
        {
            return false;
        }
    }

}