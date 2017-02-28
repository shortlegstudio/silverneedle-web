// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Groups
{
    using NUnit.Framework;
    using SilverNeedle.Characters;
    using SilverNeedle.Groups;

    [TestFixture]
    public class SettlementTests 
    {
        [Test]
        public void SettlementsHaveManyCharactersInThem()
        {
            var settlement = new Settlement();
            settlement.AddInhabitant(new CharacterSheet());
            settlement.AddInhabitant(new CharacterSheet());

            Assert.AreEqual(2, settlement.Population);
        }
    }
}