// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using SilverNeedle.Utility;
    public class JackOfAllTrades : SpecialAbility, IComponent, IImprovesWithLevels
    {
        public void Initialize(ComponentBag components)
        {
            var bardLevel = components.Get<ClassLevel>();
            var skillRanks = components.Get<SkillRanks>();
            UpdateSkills(skillRanks, bardLevel.Level);
        }

        public void LeveledUp(ComponentBag components)
        {
            var bardLevel = components.Get<ClassLevel>();
            var skillRanks = components.Get<SkillRanks>();
            UpdateSkills(skillRanks, bardLevel.Level);
        }

        private void UpdateSkills(SkillRanks skillRanks, int bardLevel)
        {
            foreach(var skill in skillRanks.GetSkills())
            {
                if(bardLevel >= 10)
                    skill.CanUseWithoutTraining();
                
                if(bardLevel >=16)
                    skill.ClassSkill = true;
            }
        }
    }
}