// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using NUnit.Framework;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;

    [TestFixture]
    public class TrackTests
    {
        [Test]
        public void AddsHalfRangerLevelToTrackingSurvivalChecks()
        {
            var character = new CharacterSheet();
            var ranger = new Class();
            ranger.Name = "Ranger";
            character.SkillRanks.FillSkills(new Skill[] { new Skill("Survival", AbilityScoreTypes.Wisdom, false) }, character.AbilityScores);
            character.SetClass(ranger);
            character.SetLevel(4);

            var track = new Track();
            character.Add(track);
            var survivalSkill = character.GetSkill("Survival");
            Assert.That(survivalSkill.GetConditionalScore("track"), Is.EqualTo(survivalSkill.Score() + 2));
            Assert.That(track.Name, Is.EqualTo("Track +2"));
        }
    }
}