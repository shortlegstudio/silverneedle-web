// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Characters.SpecialAbilities
{
    using System.Linq;
    using SilverNeedle.Utility;
    public class BardicKnowledge : SpecialAbility, IComponent
    {
        public void Initialize(ComponentBag components)
        {
            var bard = components.Get<ClassLevel>();
            var skills = components.Get<SkillRanks>();
            var knowledgeSkills = skills.GetSkills().Where(x => x.Name.Contains("Knowledge"));
            foreach(var knowledge in knowledgeSkills)
            {
                knowledge.AddModifier(
                    new DelegateStatModifier(knowledge.Name,
                        "bonus",
                        this.Name,
                        () => { return MathHelpers.AtLeast(bard.Level / 2, 1); })
                );
                knowledge.CanUseWithoutTraining();
            }
        }
    }
}