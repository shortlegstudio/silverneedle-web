// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Prerequisites
{
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    public class SkillRankPrerequisite : IPrerequisite
    {
        public SkillRankPrerequisite(IObjectStore configuration)
        {
            configuration.Deserialize(this);
        }

        [ObjectStore("name")]
        public string SkillName { get; set; }

        [ObjectStore("minimum")]
        public int Minimum { get; set; }

        public bool IsQualified(ComponentContainer components)
        {
            var stat = components.FindStat<CharacterSkill>(SkillName);

            if(stat == null)
                throw new StatisticNotFoundException(SkillName);

            return stat.Ranks >= Minimum;
        }
    }
}