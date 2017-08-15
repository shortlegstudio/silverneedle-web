// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;

    
    public class TrackTests
    {
        [Fact]
        public void AddsHalfRangerLevelToTrackingSurvivalChecks()
        {
            var character = new CharacterSheet();
            var ranger = new Class();
            ranger.Name = "Ranger";
            character.SkillRanks.AddSkill(new Skill("Survival", AbilityScoreTypes.Wisdom, false));
            character.SetClass(ranger);
            character.SetLevel(4);

            var track = new Track();
            character.Add(track);
            var survivalSkill = character.SkillRanks.GetSkill("Survival");
            Assert.Equal(survivalSkill.GetConditionalScore("track"), survivalSkill.Score() + 2);
            Assert.Equal(track.Name, "Track +2");
        }
    }
}