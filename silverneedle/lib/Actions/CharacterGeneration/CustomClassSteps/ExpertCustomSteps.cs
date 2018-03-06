// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.CharacterGeneration.CustomClassSteps
{
    using SilverNeedle.Characters;

    public class ExpertCustomSteps : IFeatureCommand
    {
        public void Execute(Utility.ComponentContainer components)
        {
            // Randomly pick 10 and gopherit
            var skillRanks = components.Get<SkillRanks>();
            var skills = skillRanks.GetSkills().Choose(10);
            foreach(var s in skills)
            {
                skillRanks.SetClassSkill(s.Name);
            }
        }
    }
}