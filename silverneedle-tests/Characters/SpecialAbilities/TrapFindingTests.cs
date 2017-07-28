// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;

    
    public class TrapFindingTests
    {
        [Fact]
        public void AddsModifiersToThePerceptionAndDisableDeviceSkills()
        {
            var character = new CharacterSheet();
            character.SetClass(new Class("Rogue"));
            character.SetLevel(4);
            character.SkillRanks.FillSkills(
                new Skill[] { 
                    new Skill("Perception", AbilityScoreTypes.Wisdom, false),
                    new Skill("Disable Device", AbilityScoreTypes.Dexterity, false) }, 
                character.AbilityScores);
                
            var trapFinding = new TrapFinding();
            character.Add(trapFinding);

            var disable = character.GetSkill("Disable Device");
            var perception = character.GetSkill("Perception");
            Assert.Equal(disable.GetConditionalScore("traps"), disable.Score() + 2);
            Assert.Equal(perception.GetConditionalScore("traps"), perception.Score() + 2);
        }
    }
}