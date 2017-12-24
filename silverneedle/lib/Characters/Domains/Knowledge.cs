// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Domains
{
    using System;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    public class Knowledge : Domain, IComponent, IImprovesWithLevels
    {
        private ClassLevel source;
        private LoreKeeper loreKeeper;
        private RemoteViewing remoteView;
        public Knowledge(IObjectStore data) : base(data)
        {
            
        }

        public void Initialize(ComponentContainer components)
        {
            source = components.Get<ClassLevel>();
            loreKeeper = new LoreKeeper();
            components.Add(loreKeeper);

            var skills = components.Get<SkillRanks>();
            foreach(var s in skills.GetSkills())
            {
                if(s.Skill.IsKnowledgeSkill)
                {
                    s.ClassSkill = true;
                }
            }
        }

        public void LeveledUp(ComponentContainer components)
        {
            if(source.Level == 6)
            {
                remoteView = new RemoteViewing();
                components.Add(remoteView);
            }
        }
    }
}