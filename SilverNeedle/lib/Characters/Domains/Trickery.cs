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

    public class Trickery : Domain, IComponent, IImprovesWithLevels
    {
        private ClassLevel source;
        private Copycat copycat;
        private MasterIllusion illusion;
        public Trickery(IObjectStore data) : base(data)
        {
            
        }

        public void Initialize(ComponentBag components)
        {
            source = components.Get<ClassLevel>();
            copycat = new Copycat(components.Get<AbilityScores>().GetAbility(AbilityScoreTypes.Wisdom));
            components.Add(copycat);
            var skills = components.Get<SkillRanks>();
            skills.SetClassSkill("Bluff");
            skills.SetClassSkill("Disguise");
            skills.SetClassSkill("Stealth");
        }

        public void LeveledUp(ComponentBag components)
        {
            if(source.Level == 8)
            {
                illusion = new MasterIllusion();
                components.Add(illusion);
            }
        }
    }
}