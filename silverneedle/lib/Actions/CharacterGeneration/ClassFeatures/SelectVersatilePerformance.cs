// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Actions.CharacterGeneration.ClassFeatures
{
    using System.Linq;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    public class SelectVersatilePerformance : ICharacterDesignStep
    {
        public void ExecuteStep(CharacterSheet character)
        {
            var versatilePerformance = character.Get<VersatilePerformance>();
            var performSkills = character.SkillRanks.GetSkills()
                .Where(x => x.Name.Contains("Perform") && !versatilePerformance.Skills.Contains(x))
                .GroupBy(x => x.Score());
            var highestGroup = performSkills.Max(x => x.Key);
            
            versatilePerformance.AddSkill(performSkills.First(x => x.Key == highestGroup).ChooseOne());
        }
    }
}