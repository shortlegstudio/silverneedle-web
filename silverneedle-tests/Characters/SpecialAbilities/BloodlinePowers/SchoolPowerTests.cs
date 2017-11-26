// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities.BloodlinePowers
{
    using Xunit;
    using System.Linq;
    using SilverNeedle.Characters.Magic;
    using SilverNeedle.Characters.SpecialAbilities.BloodlinePowers;

    public class SchoolPowerTests : RequiresDataFiles
    {
        [Fact]
        public void PicksASchoolToSuperCharge()
        {
            var sorcerer = CharacterTestTemplates.Sorcerer();
            var school = new SchoolPower();
            sorcerer.Add(school);
            Assert.NotNull(school.School);
            Assert.Equal("School Power (+2 DC for " + school.School.Name + " spells)", school.Name);
        }
    }
}