// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.CharacterGeneration.CustomClassSteps
{
    using SilverNeedle.Characters;

    public class ExpertCustomSteps : ICharacterDesignStep
    {
        public void ExecuteStep(CharacterSheet character, CharacterStrategy strategy)
        {
            // Randomly pick 10 and gopherit
            var skills = character.SkillRanks.GetSkills().Choose(10);
            foreach(var s in skills)
            {
                character.SkillRanks.SetClassSkill(s.Name);
            }
        }
    }
}