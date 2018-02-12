// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters
{
    using SilverNeedle.Characters;
    using SilverNeedle.Serialization;
    using Xunit;

    public class SkillRankDependentModifierTests
    {
        private CharacterSheet bob;
        private SkillRankDependentModifier modifier;

        private void SetupCharacterWithAcrobaticsDependentModifier()
        {
            bob = CharacterTestTemplates.AverageBob().WithSkills();

            var yaml = @"
name: Acrobatics
skill: Acrobatics
minimum-ranks: 10
modifier: 2
modifier-type: bonus
";
            modifier = new SkillRankDependentModifier(yaml.ParseYaml());
            bob.Add(modifier);
        }

        [Fact]
        public void ProvidesNothingIfMinimumRanksIsNotMet()
        {
            SetupCharacterWithAcrobaticsDependentModifier();
            bob.SkillRanks.GetSkill("Acrobatics").AddRank();
            Assert.Equal(0, modifier.Modifier);
        }

        [Fact]
        public void ProvidesBonusUponReachingMinimumRanks()
        {
            SetupCharacterWithAcrobaticsDependentModifier();
            SilverNeedle.Utility.Repeat.Times(10, () => 
                bob.SkillRanks.GetSkill("Acrobatics").AddRank()
            );
            Assert.Equal(2, modifier.Modifier);
        }
    }
}