// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

using System.Linq;
using Xunit;
using SilverNeedle;
using SilverNeedle.Characters;

namespace Tests.Characters {

    
    public class CharacterSkillTests {
        [Fact]
        public void UntrainedSkillsAreBasedOffOfAttributeScore() {
            //Set up a skill
            var skill = new Skill (
                "Climb",
                AbilityScoreTypes.Strength,
                false
            );

            var charSkill = new CharacterSkill (skill, new AbilityScore(AbilityScoreTypes.Strength, 14), false);
            Assert.Equal (2, charSkill.Score());
            Assert.True (charSkill.AbleToUse);
        }

        [Fact]
        public void TrainedSkillsStartAtMinValueAndUnableToUse() {
            var skill = new Skill (
                            "Disable Device",
                            AbilityScoreTypes.Dexterity,
                            true
                        );
            var charSkill = new CharacterSkill (skill, new AbilityScore(AbilityScoreTypes.Dexterity, 18), false);
            Assert.Equal (int.MinValue, charSkill.Score());
            Assert.False (charSkill.AbleToUse);
        }

        [Fact]
        public void SometimesTrainedSkillNeedsToBeFlaggedAsAllowedByACharacter()
        {

            var skill = new Skill (
                            "Disable Device",
                            AbilityScoreTypes.Dexterity,
                            true
                        );
            var charSkill = new CharacterSkill (skill, new AbilityScore(AbilityScoreTypes.Dexterity, 18), false);
            Assert.False (charSkill.AbleToUse);
            charSkill.CanUseWithoutTraining();
            Assert.True(charSkill.AbleToUse);
        }
        [Fact]
        public void AddingPointsToSkillsIncreasesTheirScore() {
            var skill = new Skill (
                            "Swim",
                            AbilityScoreTypes.Strength,
                            false
            );
            var charSkill = new CharacterSkill (skill, new AbilityScore(AbilityScoreTypes.Strength, 15), false);
            var baseValue = charSkill.Score();
            charSkill.AddRank ();
            Assert.Equal (1, charSkill.Ranks);
            Assert.Equal (baseValue + 1, charSkill.Score());
        }

        [Fact]
        public void AddingARankAllowsToUseTrainingSkill() {
            var skill = new Skill (
                            "Spellcraft",
                            AbilityScoreTypes.Intelligence,
                            true
                        );
            var charSkill = new CharacterSkill (skill, new AbilityScore(AbilityScoreTypes.Intelligence, 15), false);
            Assert.False (charSkill.AbleToUse);
            charSkill.AddRank ();
            Assert.True (charSkill.AbleToUse);
            Assert.Equal (3, charSkill.Score());
        }

        [Fact]
        public void ClassSkillsGetOneTimeBonus() {
            var skill = new Skill (
                            "Climb",
                            AbilityScoreTypes.Strength,
                            false
                        );
            var charSkill = new CharacterSkill (skill, new AbilityScore(AbilityScoreTypes.Strength, 10), true);
            charSkill.ClassSkill = true;
            charSkill.AddRank ();
            Assert.Equal (4, charSkill.Score());
            charSkill.AddRank ();
            Assert.Equal (5, charSkill.Score());
        }

        [Fact]
        public void SkillsCanHaveAdjustmentsFromTraitsOrFeats() {
            var adjust = new ValueStatModifier (
                            "Fly",
                            2,
                            "Bonus");
            var flySkill = new Skill ("Fly", AbilityScoreTypes.Dexterity, false);
            var charSkill = new CharacterSkill (flySkill, new AbilityScore (AbilityScoreTypes.Dexterity, 10), false);
            charSkill.AddModifier (adjust);

            Assert.Equal (2, charSkill.Score());
        }

        [Fact]
        public void SkillsRecalculateWhenAbilityIsUpdated() {
            var skill = new Skill ("Chew", AbilityScoreTypes.Strength, false);
            var ability = new AbilityScore (AbilityScoreTypes.Strength, 10);
            var charSkill = new CharacterSkill (skill, ability, false);

            var oldVal = charSkill.Score();
            var adjustment = new ValueStatModifier(2);
            adjustment.Modifier = 6;
            ability.AddModifier(adjustment);
            Assert.True(charSkill.Score() > oldVal);

        }

        [Fact]
        public void ModificationToAnAdjustmentAreReflectedInTotalScore() {
            var skill = new Skill ("Chew", AbilityScoreTypes.Strength, false);
            var ability = new AbilityScore (AbilityScoreTypes.Strength, 10);
            var charSkill = new CharacterSkill (skill, ability, false);
            var adj = new ValueStatModifier ("Chew", 0, "Teeth");
            charSkill.AddModifier (adj);
            Assert.Equal (0, charSkill.Score());
            adj.Modifier = 5;
            Assert.Equal (5, charSkill.Score());
        }

        [Fact]
        public void SkillsCanHaveConditionalModifiers() {
            var skill = new Skill("Eat", AbilityScoreTypes.Intelligence, false);
            var ability = new AbilityScore(AbilityScoreTypes.Intelligence, 10);
            var charSkill = new CharacterSkill(skill, ability, false);
            var adj = new ConditionalStatModifier(new ValueStatModifier("Eat", 3, "bonus"), "Celery");
            charSkill.AddModifier(adj);
            Assert.Equal(1, charSkill.ConditionalModifiers.Count());
            Assert.Equal(3, charSkill.GetConditionalValue("Celery"));
            Assert.Equal(0, charSkill.Score());
            Assert.Equal("Eat +0 (+3 Celery)", charSkill.ToString());
        }

        [Fact]
        public void ConditionalModifiersAndRanksAreCountedOnlyOnce() {
            var skill = new Skill("Eat", AbilityScoreTypes.Intelligence, false);
            var ability = new AbilityScore(AbilityScoreTypes.Intelligence, 10);
            var charSkill = new CharacterSkill(skill, ability, false);
            charSkill.AddRank();
            var adj = new ConditionalStatModifier(new ValueStatModifier("Eat", 3, "bonus"), "Celery");
            charSkill.AddModifier(adj);
            Assert.Equal(1, charSkill.ConditionalModifiers.Count());
            Assert.Equal(4, charSkill.GetConditionalValue("Celery"));
            Assert.Equal(1, charSkill.Score());
            Assert.Equal("Eat +1 (+4 Celery)", charSkill.ToString());
        }
    }
}
