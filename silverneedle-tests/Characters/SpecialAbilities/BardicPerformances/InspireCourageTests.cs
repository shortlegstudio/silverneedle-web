// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities.BardicPerformances
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities.BardicPerformances;
    public class InspireCourageTests : RequiresDataFiles
    {
        [Fact]
        public void GrantsABonusOfOneAtFirstLevel()
        {
            var inspire = new InspireCourage();
            var bard = CharacterTestTemplates.BardyBard();
            bard.Add(inspire);
            Assert.Equal(1, inspire.Bonus);
            Assert.Equal("Inspire Courage +1", inspire.Description);
        }

        [Fact]
        public void ImprovesAtSpecificLevels()
        {
            var inspire = new InspireCourage();
            var bard = CharacterTestTemplates.BardyBard();
            bard.Add(inspire);
            bard.SetLevel(5);
            Assert.Equal(2, inspire.Bonus);
            Assert.Equal("Inspire Courage +2", inspire.Description);
            bard.SetLevel(11);
            Assert.Equal(3, inspire.Bonus);
            Assert.Equal("Inspire Courage +3", inspire.Description);
            bard.SetLevel(17);
            Assert.Equal(4, inspire.Bonus);
            Assert.Equal("Inspire Courage +4", inspire.Description);
        }
    }
}