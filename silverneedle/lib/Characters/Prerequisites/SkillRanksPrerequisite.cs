// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Prerequisites
{
    using SilverNeedle.Utility;

    public class SkillRankPrerequisite : IPrerequisite
    {
        public SkillRankPrerequisite(string value)
        {
            var vals = value.Split(' ');
            this.SkillName = vals[0];
            this.Minimum = int.Parse(vals[1]);
        }

        public string SkillName { get; set; }

        public int Minimum { get; set; }

        public bool IsQualified(ComponentContainer components)
        {
            return false;
        }
    }
}