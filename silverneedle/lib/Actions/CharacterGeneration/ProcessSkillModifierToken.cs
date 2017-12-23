// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Actions.CharacterGeneration
{
    using System.Linq;
    using SilverNeedle.Characters;
    public class ProcessSkillModifierToken : ICharacterDesignStep
    {
        public void ExecuteStep(CharacterSheet character)
        {
            var tokens = character.GetAll<SkillModifierToken>();

            foreach(var token in tokens)
            {
                var matchingSkills = character.SkillRanks.GetSkills().Where(x => token.Qualifies(x.Skill));
                var chosen = matchingSkills.ChooseOne();
                ShortLog.DebugFormat("Skill Modifier Token assigned to {0}", chosen.Skill.Name);
                chosen.AddModifier(token.CreateModifier(chosen.Skill));
            }
        }
    }
}