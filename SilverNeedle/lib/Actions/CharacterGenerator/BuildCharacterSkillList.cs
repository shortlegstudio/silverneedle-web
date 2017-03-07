// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

using System;
using SilverNeedle.Characters;

namespace SilverNeedle.Actions.CharacterGenerator
{
    public class BuildCharacterSkillList : ICharacterBuildStep
    {
        IEntityGateway<Skill> skills;

        public BuildCharacterSkillList()
        {
            skills = GatewayProvider.Instance().Skills;
        }

        public void ProcessFirstLevel(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            character.SkillRanks.FillSkills(skills.All(), character.AbilityScores);
        }

        public void ProcessLevelUp(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            throw new NotImplementedException();
        }
    }
}