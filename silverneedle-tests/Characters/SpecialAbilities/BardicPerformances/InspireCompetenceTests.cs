// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities.BardicPerformances
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities.BardicPerformances;
    public class InspireCompetenceTests
    {
        [Fact]
        public void GrantsABonusOfTwoAtThirdLeel()
        {
            var inspire = new InspireCompetence();
            var bard = CharacterTestTemplates.BardyBard();
            bard.SetLevel(3);
            bard.Add(inspire);
            Assert.Equal(2, inspire.Bonus);
            Assert.Equal("Inspire Competence +2", inspire.Description);
        }

        [Fact]
        public void ImprovesAtSpecificLevels()
        {
            var inspire = new InspireCompetence();
            var bard = CharacterTestTemplates.BardyBard();
            bard.Add(inspire);
            bard.SetLevel(7);
            Assert.Equal(3, inspire.Bonus);
            Assert.Equal("Inspire Competence +3", inspire.Description);
            bard.SetLevel(11);
            Assert.Equal(4, inspire.Bonus);
            Assert.Equal("Inspire Competence +4", inspire.Description);
            bard.SetLevel(15);
            Assert.Equal(5, inspire.Bonus);
            Assert.Equal("Inspire Competence +5", inspire.Description);
        }
    }
}