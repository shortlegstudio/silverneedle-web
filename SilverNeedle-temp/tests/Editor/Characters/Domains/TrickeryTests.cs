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

    public class TrickeryTests : DomainTestBase<Trickery>
    {
        [SetUp]
        public void Configure()
        {
            base.InitializeDomain("trickery");
        }

        [Fact]
        public void Copycat()
        {
            var touch = character.Get<Copycat>();
            Assert.That(touch, Is.Not.Null); 
            Assert.That(touch.UsesPerDay, Is.EqualTo(6));
        }

        [Fact]
        public void MasterIllusion()
        {
            character.SetLevel(8);
            domain.LeveledUp(character.Components);
            var aura = character.Get<MasterIllusion>();
            Assert.That(aura, Is.Not.Null); 
        }

        [Fact]
        public void ExtraClassSkills()
        {
            Assert.That(character.GetSkill("Bluff").ClassSkill, Is.True);
            Assert.That(character.GetSkill("Disguise").ClassSkill, Is.True);
            Assert.That(character.GetSkill("Stealth").ClassSkill, Is.True);
        }
    }
}