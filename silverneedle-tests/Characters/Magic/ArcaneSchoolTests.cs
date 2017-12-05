// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.Magic
{
    using System.Linq;
    using Xunit;
    using SilverNeedle.Characters.Magic;
    using SilverNeedle.Utility;

    public class ArcaneSchoolTests
    {
        [Fact]
        public void ProvidesAListOfAbilitiesForSchool()
        {
            var configuration = @"---
name: Abjuration
abilities:
  - ability: SomePowerString1
    level: 1
  - ability: SomePowerString2
    level: 4
".ParseYaml();

            var arcaneSchool = new ArcaneSchool(configuration);
            var abilities = arcaneSchool.GetAbilities();
            Assert.Equal(new string[] { 
                "SomePowerString1",
                "SomePowerString2"
                }, abilities.Select(b => b.GetString("ability"))
            );
                Assert.Equal(new int[] {
                1, 4
                }, abilities.Select(p => p.GetInteger("level"))
             );

        }

        [Fact]
        public void UniversalistIsFlaggedToAvoidOppositionSchools()
        {

            var configuration = @"---
name: Universalist
no-opposition-schools: true
abilities:
  - ability: SomePowerString1
    level: 1
".ParseYaml();
            var school = new ArcaneSchool(configuration);
            Assert.True(school.NoOppositionSchools);
        }
    }
}