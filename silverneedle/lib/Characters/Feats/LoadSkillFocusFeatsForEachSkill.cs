// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Characters.Feats
{
    using System.Collections.Generic;
    using SilverNeedle.Serialization;
    public class LoadSkillFocusFeatsForEachSkill : IGatewayLoader<Feat>
    {
        public IEnumerable<Feat> Load(IObjectStore configuration)
        {
            var skillFocuses = new List<SkillFocus>();
            //Set one up as the "Generic" one for use in trait situations
            var skillFocusPrimary = new SkillFocus(configuration);
            var skills = GatewayProvider.All<Skill>();

            skillFocuses.Add(skillFocusPrimary);
            foreach(var skl in skills)
            {
                var copy = skillFocusPrimary.Copy() as SkillFocus;
                copy.SetSkillFocus(skl.Name);
                copy.Tags.Add(FeatToken.IGNORE_GENERIC_TAG);
                skillFocuses.Add(copy);
            }

            return skillFocuses;
        }
    }
}