// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.Magic
{
    using System.Linq;
    using Xunit;
    using SilverNeedle.Characters.Magic;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    public class ArcaneSchoolTests
    {
        public void UniversalistIsFlaggedToAvoidOppositionSchools()
        {

            var configuration = @"---
name: Universalist
no-opposition-schools: true
levels:
  - level: 1
    attributes:
      - attribute:
        name: Some Ability
".ParseYaml();
            var school = new ArcaneSchool(configuration);
            Assert.True(school.NoOppositionSchools);
        }
    }
}