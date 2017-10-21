// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Characters.Feats
{
    using SilverNeedle.Utility;
    public class SkillFocus : Feat, IComponent
    {
        private DelegateStatModifier statModifier;
        public void Initialize(ComponentBag components)
        {
            var strategy = components.Get<CharacterStrategy>();
            var skills = components.Get<SkillRanks>();
            if(strategy.FavoredSkills.IsEmpty)
            {
                var skill = skills.GetSkills().ChooseOne();
                ApplyBonusToSkill(skill);
            }
            else
            {
                var skillName = strategy.FavoredSkills.ChooseRandomly();
                var skill = skills.GetSkill(skillName);
                ApplyBonusToSkill(skill);
            }
        }

        private void ApplyBonusToSkill(CharacterSkill skill)
        {
            statModifier = new DelegateStatModifier(
                skill.Name,
                "bonus",
                this.Name,
                () => {
                    return skill.Ranks >= 10 ? 6 : 3;
                }
            );
            skill.AddModifier(statModifier);
        }
    }
}