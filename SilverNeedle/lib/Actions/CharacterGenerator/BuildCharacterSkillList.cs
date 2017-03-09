// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

using System;
using SilverNeedle.Characters;

namespace SilverNeedle.Actions.CharacterGenerator
{
    public class BuildCharacterSkillList : ICreateStep
    {
        IEntityGateway<Skill> skills;

        public BuildCharacterSkillList()
        {
            skills = GatewayProvider.Get<Skill>();
        }

        public void Process(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            character.SkillRanks.FillSkills(skills.All(), character.AbilityScores);
        }
    }
}