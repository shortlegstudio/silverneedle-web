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

    public class TrickeryTests : DomainTestBase<Trickery>
    {
        public TrickeryTests()
        {
            base.InitializeDomain("trickery");
        }

        [Fact]
        public void Copycat()
        {
            var touch = character.Get<Copycat>();
            Assert.NotNull(touch);
            Assert.Equal(touch.UsesPerDay, 6);
        }

        [Fact]
        public void MasterIllusion()
        {
            character.SetLevel(8);
            domain.LeveledUp(character.Components);
            var aura = character.Get<MasterIllusion>();
            Assert.NotNull(aura);
        }

        [Fact]
        public void ExtraClassSkills()
        {
            Assert.True(character.SkillRanks.GetSkill("Bluff").ClassSkill);
            Assert.True(character.SkillRanks.GetSkill("Disguise").ClassSkill);
            Assert.True(character.SkillRanks.GetSkill("Stealth").ClassSkill);
        }
    }
}