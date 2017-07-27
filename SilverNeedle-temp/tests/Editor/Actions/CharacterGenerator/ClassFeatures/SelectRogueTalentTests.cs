// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGenerator.ClassFeatures
{
    using NUnit.Framework;
    using SilverNeedle.Actions.CharacterGenerator.ClassFeatures;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Serialization;

    [TestFixture]
    public class SelectRogueTalentTests
    {
        EntityGateway<RogueTalent> Talents;
        RogueTalent basicTalent;
        RogueTalent advancedTalent;
        [SetUp]
        public void Configure()
        {
            var basicData = new MemoryStore();
            basicData.SetValue("name", "Basic Talent");
            basicTalent = new RogueTalent(basicData);

            var advData = new MemoryStore();
            advData.SetValue("name", "Advanced Talent");
            advData.SetValue("advanced-talent", "true");
            advancedTalent = new RogueTalent(advData);

            Talents = new EntityGateway<RogueTalent>(new RogueTalent[] {basicTalent, advancedTalent});
        }
        [Fact]
        public void SelectsBasicTalentsIfNotConfiguredForAdvancedTalents()
        {
            var subject = new SelectRogueTalent(new MemoryStore(), Talents);
            var character = new CharacterSheet();
            subject.Process(character, new CharacterBuildStrategy());
            var rogueTalent = character.Get<RogueTalent>();
            Assert.That(rogueTalent.IsAdvancedTalent, Is.False);
            Assert.That(rogueTalent, Is.EqualTo(basicTalent));
        }

        [Fact]
        public void SelectAdvancedTalentPrefersAdvancedTalents()
        {
            var configure = new MemoryStore();
            configure.SetValue("advanced-talents", "true");
            var subject = new SelectRogueTalent(configure, Talents);
            var character = new CharacterSheet();
            subject.Process(character, new CharacterBuildStrategy());
            var rogueTalent = character.Get<RogueTalent>();
            Assert.That(rogueTalent.IsAdvancedTalent, Is.True);
            Assert.That(rogueTalent, Is.EqualTo(advancedTalent));
        }

        [Fact]
        public void SelectsNonDuplicateTalents()
        {
            var configure = new MemoryStore();
            configure.SetValue("advanced-talents", "true");
            var subject = new SelectRogueTalent(configure, Talents);
            var character = new CharacterSheet();
            character.Add(advancedTalent);
            subject.Process(character, new CharacterBuildStrategy());
            var rogueTalents = character.GetAll<RogueTalent>();
            Assert.That(rogueTalents, Is.EquivalentTo(new RogueTalent[] { basicTalent, advancedTalent }));
        }
    }
}