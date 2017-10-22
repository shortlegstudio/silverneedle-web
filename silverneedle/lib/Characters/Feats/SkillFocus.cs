// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Characters.Feats
{
    using System.Collections.Generic;
    using System.Linq;
    using SilverNeedle.Serialization;   
    using SilverNeedle.Utility;

    public class SkillFocus : Feat, IComponent
    {
        private DelegateStatModifier statModifier;
        public CharacterSkill CharacterSkill { get; private set; }
        public SkillFocus()
        {
            this.Name = "Skill Focus";
        }
        public SkillFocus(IObjectStore configure) : base(configure) { }
        public void Initialize(ComponentBag components)
        {
            var strategy = components.Get<CharacterStrategy>();
            var skills = components.Get<SkillRanks>();
            var skillTable = GetSkillTable(skills, strategy);
            DisableOptionsThatAlreadyHaveSkillFocus(skillTable, components.GetAll<SkillFocus>());
            var skillName = skillTable.ChooseRandomly();
            this.CharacterSkill = skills.GetSkill(skillName);
            ApplyBonusToSkill();
        }

        private WeightedOptionTable<string> GetSkillTable(SkillRanks skills, CharacterStrategy strategy)
        {
            if(strategy.FavoredSkills.IsEmpty)
            {
                return skills.GetSkills().Select(x => x.Name).CreateFlatTable();
            }
            else
            {
                return strategy.FavoredSkills;
            }
        }

        private void DisableOptionsThatAlreadyHaveSkillFocus(WeightedOptionTable<string> skillTable, IEnumerable<SkillFocus> existingSkillFocuses)
        {
            foreach(var sf in existingSkillFocuses)
            {
                if(sf != this)
                {
                    skillTable.Disable(sf.CharacterSkill.Name);
                }
            }
        }

        private void ApplyBonusToSkill()
        {
            statModifier = new DelegateStatModifier(
                this.CharacterSkill.Name,
                "bonus",
                this.Name,
                () => {
                    return this.CharacterSkill.Ranks >= 10 ? 6 : 3;
                }
            );
            this.CharacterSkill.AddModifier(statModifier);
        }
    }
}