// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters
{
    using System.Linq;
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    public class FillSkillsTests 
    {
        [Fact]
        public void InitializesAndAddsAllTheSkillsToTheComponentContainer()
        {
            var skills = EntityGateway<Skill>.LoadFromList(new Skill[] {
                new Skill("Climb", AbilityScoreTypes.Strength, false),
                new Skill("Swim", AbilityScoreTypes.Strength, false),
                new Skill("Disable Device", AbilityScoreTypes.Dexterity, true)
            });

            var components = new ComponentContainer();
            components.Add(new AbilityScores());
            components.Add(new AbilityScore(AbilityScoreTypes.Strength, 10));
            components.Add(new AbilityScore(AbilityScoreTypes.Dexterity, 10));
            components.Add(new AbilityScore(AbilityScoreTypes.Constitution, 10));
            components.Add(new AbilityScore(AbilityScoreTypes.Intelligence, 10));
            components.Add(new AbilityScore(AbilityScoreTypes.Wisdom, 10));
            components.Add(new AbilityScore(AbilityScoreTypes.Charisma, 10));

            var fillSkills = new FillSkills(skills);
            fillSkills.Execute(components);

            Assert.Equal(3, components.GetAll<CharacterSkill>().Count());
        }
    }
}