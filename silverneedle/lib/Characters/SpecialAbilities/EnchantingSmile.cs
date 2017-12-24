// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using SilverNeedle.Utility;
    public class EnchantingSmile : SpecialAbility, IComponent
    {
        private IStatModifier skillBonusModifier;
        private ClassLevel sourceLevel;
        public void Initialize(ComponentContainer components)
        {
            sourceLevel = components.Get<ClassLevel>();
            skillBonusModifier = new DelegateStatModifier(
                "Interaction Skills",
                "enhancement",
                this.Name,
                () => { return 2 + (sourceLevel.Level / 5); }
            );
            var skills = components.Get<SkillRanks>();
            skills.GetSkill("bluff").AddModifier(skillBonusModifier);
            skills.GetSkill("diplomacy").AddModifier(skillBonusModifier);
            skills.GetSkill("intimidate").AddModifier(skillBonusModifier);
        }
    }
}