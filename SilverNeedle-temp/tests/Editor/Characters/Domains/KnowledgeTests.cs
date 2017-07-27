// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.Domains
{
    using NUnit.Framework;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Domains;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Serialization;

    [TestFixture]
    public class KnowledgeTests : DomainTestBase<Knowledge>
    {
        [SetUp]
        public void Configure()
        {
            base.InitializeDomain("knowledge");
        }

        [Fact]
        public void LoreKeepers()
        {
            var loreKeep = character.Get<LoreKeeper>();
            Assert.That(loreKeep, Is.Not.Null); 
        }

        [Fact]
        public void RemoteViewing()
        {
            character.SetLevel(6);
            domain.LeveledUp(character.Components);
            var remoteView = character.Get<RemoteViewing>();
            Assert.That(remoteView, Is.Not.Null); 
        }

        [Fact]
        public void AllKnowledgeSkillsAreClassSkills()
        {
            var allSkills = character.SkillRanks.GetSkills();
            foreach(var skill in allSkills)
            {
                if(skill.Name.Contains("Knowledge"))
                {
                    Assert.That(skill.ClassSkill, Is.True);
                } 
            }
        }
    }
}