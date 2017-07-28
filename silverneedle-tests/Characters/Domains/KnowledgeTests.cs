// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.Domains
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Domains;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Serialization;

    
    public class KnowledgeTests : DomainTestBase<Knowledge>
    {
        public KnowledgeTests()
        {
            base.InitializeDomain("knowledge");
        }

        [Fact]
        public void LoreKeepers()
        {
            var loreKeep = character.Get<LoreKeeper>();
            Assert.NotNull(loreKeep); 
        }

        [Fact]
        public void RemoteViewing()
        {
            character.SetLevel(6);
            domain.LeveledUp(character.Components);
            var remoteView = character.Get<RemoteViewing>();
            Assert.NotNull(remoteView); 
        }

        [Fact]
        public void AllKnowledgeSkillsAreClassSkills()
        {
            var allSkills = character.SkillRanks.GetSkills();
            foreach(var skill in allSkills)
            {
                if(skill.Name.Contains("Knowledge"))
                {
                    Assert.True(skill.ClassSkill);
                } 
            }
        }
    }
}