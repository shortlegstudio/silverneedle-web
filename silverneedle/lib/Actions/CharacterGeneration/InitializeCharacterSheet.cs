// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Actions.CharacterGeneration
{
    using SilverNeedle.Characters;
    using SilverNeedle.Serialization;

    public class InitializeCharacterSheet : ICharacterDesignStep
    {
        EntityGateway<Skill> skills;

        public InitializeCharacterSheet()
        {
            skills = GatewayProvider.Get<Skill>();
        }

        public void ExecuteStep(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            character.SkillRanks.FillSkills(skills.All());
            character.InitializeComponents();
        }
    }
}