// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;

    public class WellVersedTests
    {
        [Fact]
        public void ImproveSavingThrowsAgainstSonicAffects()
        {
            var bard = CharacterTestTemplates.BardyBard();
            bard.Add(new WellVersed());
            Assert.Equal(4, bard.Defense.FortitudeSave.GetConditionalValue("sonic"));
            Assert.Equal(4, bard.Defense.FortitudeSave.GetConditionalValue("bardic performance"));
            Assert.Equal(4, bard.Defense.FortitudeSave.GetConditionalValue("language-dependent"));
            Assert.Equal(4, bard.Defense.ReflexSave.GetConditionalValue("sonic"));
            Assert.Equal(4, bard.Defense.ReflexSave.GetConditionalValue("bardic performance"));
            Assert.Equal(4, bard.Defense.ReflexSave.GetConditionalValue("language-dependent"));
            Assert.Equal(4, bard.Defense.WillSave.GetConditionalValue("sonic"));
            Assert.Equal(4, bard.Defense.WillSave.GetConditionalValue("bardic performance"));
            Assert.Equal(4, bard.Defense.WillSave.GetConditionalValue("language-dependent"));
        }
    }
}