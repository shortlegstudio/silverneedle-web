// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using Xunit;
    using SilverNeedle.Characters.SpecialAbilities;

    public class MetamagicMasteryTests
    {
        [Fact]
        public void UsesPerDayDeterminedByLevel()
        {
            var wizard = CharacterTestTemplates.Wizard();
            var meta = new MetamagicMastery();
            wizard.Add(meta);
            wizard.SetLevel(12);
            Assert.Equal(3, meta.UsesPerDay);
        }
    }
}