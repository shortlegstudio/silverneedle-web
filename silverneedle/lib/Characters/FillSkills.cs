// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Characters
{
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;
    public class FillSkills : IFeatureCommand
    {
        private EntityGateway<Skill> skills;

        public FillSkills() : this(GatewayProvider.Get<Skill>())
        {

        }

        public FillSkills(EntityGateway<Skill> skills)
        {
            this.skills = skills;
        }

        public void Execute(ComponentContainer components)
        {
            var abilityScores = components.Get<AbilityScores>();
            foreach(var s in skills.All())
            {
                components.Add(new CharacterSkill(
                    s,
                    abilityScores.GetAbility(s.Ability),
                    false
                ));
            }
        }
    }
}