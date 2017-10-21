// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Groups
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Groups;

    public class SettlementTests 
    {
        [Fact]
        public void SettlementsHaveManyCharactersInThem()
        {
            var settlement = new Settlement();
            settlement.AddInhabitant(new CharacterSheet(CharacterStrategy.Default()));
            settlement.AddInhabitant(new CharacterSheet(CharacterStrategy.Default()));

            Assert.Equal(2, settlement.Population);
        }
    }
}