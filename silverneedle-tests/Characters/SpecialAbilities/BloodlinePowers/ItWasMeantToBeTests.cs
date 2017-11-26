// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities.BloodlinePowers
{
    using Xunit;
    using SilverNeedle.Characters.SpecialAbilities.BloodlinePowers;

    public class ItWasMeantToBeTests
    {
        [Fact]
        public void OncePerDayUntilLevelSeventeen()
        {
            var sorcerer = CharacterTestTemplates.Sorcerer();
            var meant = new ItWasMeantToBe();
            sorcerer.Add(meant);
            Assert.Equal(1, meant.UsesPerDay);
            sorcerer.SetLevel(17);
            Assert.Equal(2, meant.UsesPerDay);
        }
    }
}