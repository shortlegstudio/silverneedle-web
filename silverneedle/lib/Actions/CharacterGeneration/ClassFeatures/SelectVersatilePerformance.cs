// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Actions.CharacterGeneration.ClassFeatures
{
    using System.Linq;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    public class SelectVersatilePerformance : ICharacterDesignStep, IFeatureCommand
    {
        public void ExecuteStep(CharacterSheet character)
        {
            Execute(character.Components);
        }
        public void Execute(Utility.ComponentContainer components)
        {
            var versatilePerformance = components.Get<VersatilePerformance>();
            var skillRanks = components.Get<SkillRanks>();
            var performSkills = skillRanks.GetSkills()
                .Where(x => x.Name.Contains("Perform") && !versatilePerformance.Skills.Contains(x))
                .GroupBy(x => x.Score());
            var highestGroup = performSkills.Max(x => x.Key);
            
            versatilePerformance.AddSkill(performSkills.First(x => x.Key == highestGroup).ChooseOne());
        }
    }
}