// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.Magic
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Magic;
    using SilverNeedle.Utility;
    using SilverNeedle.Serialization;

    public class SpontaneousCastingTests
    {
        [Fact]
        public void LoadsDetailsAboutHowTheSpellsAreManaged()
        {
            var spellCasting = new SpontaneousCasting(configuration);

        }
        IObjectStore configuration = @"
spells:
  list: bard
  type: arcane
  ability: charisma
  known: spontaneous
  per-day:
    1: [all, 1]
    2: [all, 2]
    3: [all, 3]
  spells-known:
    1: [4, 2]
    2: [5, 3]
    3: [6, 4]
".ParseYaml();
    }
}