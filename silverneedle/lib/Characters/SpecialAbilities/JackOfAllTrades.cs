// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using SilverNeedle.Utility;
    public class JackOfAllTrades : IAbility, IComponent, IImprovesWithLevels, INameByType
    {
        public string DisplayString()
        {
            return this.Name();
        }
        public void Initialize(ComponentContainer components)
        {
            var bardLevel = components.Get<ClassLevel>();
            var skillRanks = components.Get<SkillRanks>();
            UpdateSkills(skillRanks, bardLevel.Level);
        }

        public void LeveledUp(ComponentContainer components)
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