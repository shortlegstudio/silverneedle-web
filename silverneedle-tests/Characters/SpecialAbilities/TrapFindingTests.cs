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
            var character = new CharacterSheet(CharacterStrategy.Default());
            character.SetClass(new Class("Rogue"));
            character.SetLevel(4);
            character.SkillRanks.AddSkill(new Skill("Perception", AbilityScoreTypes.Wisdom, false));
            character.SkillRanks.AddSkill(new Skill("Disable Device", AbilityScoreTypes.Dexterity, false));
                
            var trapFinding = new TrapFinding();
            character.Add(trapFinding);

            var disable = character.SkillRanks.GetSkill("Disable Device");
            var perception = character.SkillRanks.GetSkill("Perception");
            Assert.Equal(disable.GetConditionalValue("traps"), disable.Score() + 2);
            Assert.Equal(perception.GetConditionalValue("traps"), perception.Score() + 2);
        }
    }
}