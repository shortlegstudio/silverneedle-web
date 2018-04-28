// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.Prerequisites
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Prerequisites;

    public class RacePrerequisiteTests : RequiresDataFiles
    {
        [Fact]
        public void MustHaveRaceThatMatchesNameToPass()
        {
            var racePrereq = new RacePrerequisite("human");
            var human = CharacterTestTemplates.AverageBob().OfRace("human");
            var dwarf = CharacterTestTemplates.AverageBob().OfRace("dwarf");
            var noRace = CharacterTestTemplates.AverageBob();

            Assert.True(racePrereq.IsQualified(human.Components));
            Assert.False(racePrereq.IsQualified(dwarf.Components));
            Assert.False(racePrereq.IsQualified(noRace.Components));
        }
    }
}