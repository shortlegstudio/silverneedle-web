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

    public class SkillFocus : Feat
    {
        private DelegateStatModifier statModifier;
        public string SkillName { get; private set; }
        public CharacterSkill CharacterSkill { get; private set; }
        public SkillFocus(IObjectStore configure) : base(configure) { }
        public SkillFocus(SkillFocus copy) : base(copy) 
        { 
            this.SkillName = copy.SkillName;
            this.Name = copy.Name;
        }
        public void SetSkillFocus(string skillName)
        {
            this.SkillName = skillName;
            this.Name = "Skill Focus({1})".Formatted(this.Name, this.SkillName.Titlize());
        }
        public override void Initialize(ComponentContainer components)
        {
            base.Initialize(components);
            if(string.IsNullOrEmpty(this.SkillName))
            {
                SelectSkillFocus(components);
            }
            var skills = components.Get<SkillRanks>();
            this.CharacterSkill = skills.GetSkill(SkillName);
            ApplyBonusToSkill();
        }

        private void SelectSkillFocus(ComponentContainer components)
        {
            var strategy = components.Get<CharacterStrategy>();
            var skills = components.Get<SkillRanks>();
            var skillTable = GetSkillTable(skills, strategy);
            DisableOptionsThatAlreadyHaveSkillFocus(skillTable, components.GetAll<SkillFocus>());
            SetSkillFocus(skillTable.ChooseRandomly());
        }

        public override Feat Copy()
        {
            return new SkillFocus(this);
        }

        private WeightedOptionTable<string> GetSkillTable(SkillRanks skills, CharacterStrategy strategy)
        {
            if(strategy.FavoredSkills.IsEmpty)
            {
                return skills.GetSkills()
                    .Where(sk => sk.AbleToUse)
                    .Select(x => x.Name)
                    .CreateFlatTable();
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
                    ShortLog.DebugFormat("Skill Focus - Disable Skill {0}", sf.CharacterSkill.Name);
                    skillTable.Disable(sf.CharacterSkill.Name);
                }
            }
        }

        private void ApplyBonusToSkill()
        {
            statModifier = new DelegateStatModifier(
                this.CharacterSkill.Name,
                "bonus",
                () => {
                    return this.CharacterSkill.Ranks >= 10 ? 6 : 3;
                }
            );
            this.CharacterSkill.AddModifier(statModifier);
        }

        public static SkillFocus CreateForTesting()
        {
            var memStore = new MemoryStore();
            memStore.SetValue("name", "Skill Focus");
            return new SkillFocus(memStore);
        }
    }
}