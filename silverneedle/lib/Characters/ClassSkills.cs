// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    public class ClassSkills : IComponent
    {
        public ClassSkills(IObjectStore configuration)
        {
            configuration.Deserialize(this);
        }

        [ObjectStore("skills")]
        public string[] SkillNames { get; private set; }

        public void Initialize(ComponentContainer components)
        {
            var sr = components.Get<SkillRanks>();
            foreach(var s in this.SkillNames)
            {
                sr.SetClassSkill(s);
            }
            ShortLog.DebugFormat("Adding Class Skills: {0}".Formatted(this.SkillNames));
        }
    }
}