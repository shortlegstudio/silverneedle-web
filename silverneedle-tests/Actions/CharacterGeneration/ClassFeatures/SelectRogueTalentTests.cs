// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGeneration.ClassFeatures
{
    using Xunit;
    using SilverNeedle.Actions.CharacterGeneration.ClassFeatures;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Serialization;

    
    public class SelectRogueTalentTests
    {
        EntityGateway<RogueTalent> Talents;
        RogueTalent basicTalent;
        RogueTalent advancedTalent;
        public SelectRogueTalentTests()
        {
            var basicData = new MemoryStore();
            basicData.SetValue("name", "Basic Talent");
            basicTalent = new RogueTalent(basicData);

            var advData = new MemoryStore();
            advData.SetValue("name", "Advanced Talent");
            advData.SetValue("advanced-talent", "true");
            advancedTalent = new RogueTalent(advData);

            Talents = EntityGateway<RogueTalent>.LoadFromList(new RogueTalent[] {basicTalent, advancedTalent});
        }
        [Fact]
        public void SelectsBasicTalentsIfNotConfiguredForAdvancedTalents()
        {
            var subject = new SelectRogueTalent(new MemoryStore(), Talents);
            var character = new CharacterSheet(CharacterStrategy.Default());
            subject.ExecuteStep(character);
            var rogueTalent = character.Get<RogueTalent>();
            Assert.False(rogueTalent.IsAdvancedTalent);
            Assert.Equal(rogueTalent, basicTalent);
        }

        [Fact]
        public void SelectAdvancedTalentPrefersAdvancedTalents()
        {
            var configure = new MemoryStore();
            configure.SetValue("advanced-talents", "true");
            var subject = new SelectRogueTalent(configure, Talents);
            var character = new CharacterSheet(CharacterStrategy.Default());
            subject.ExecuteStep(character);
            var rogueTalent = character.Get<RogueTalent>();
            Assert.True(rogueTalent.IsAdvancedTalent);
            Assert.Equal(rogueTalent, advancedTalent);
        }

        [Fact]
        public void SelectsNonDuplicateTalents()
        {
            var configure = new MemoryStore();
            configure.SetValue("advanced-talents", "true");
            var subject = new SelectRogueTalent(configure, Talents);
            var character = new CharacterSheet(CharacterStrategy.Default());
            character.Add(advancedTalent);
            subject.ExecuteStep(character);
            var rogueTalents = character.GetAll<RogueTalent>();
            Assert.NotStrictEqual(rogueTalents, new RogueTalent[] { basicTalent, advancedTalent });
        }
    }
}