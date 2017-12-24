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
        EntityGateway<CharacterSheetSetup> setups;

        public InitializeCharacterSheet()
        {
            skills = GatewayProvider.Get<Skill>();

            setups = GatewayProvider.Get<CharacterSheetSetup>();
        }

        public void ExecuteStep(CharacterSheet character)
        {
            var configure = setups.Find("default");
            character.Add(configure);
            character.SkillRanks.FillSkills(skills.All());
            character.InitializeComponents();
        }
    }
}