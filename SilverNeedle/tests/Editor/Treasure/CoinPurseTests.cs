// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Treasure
{
    using NUnit.Framework;
    using SilverNeedle.Treasure;

    [TestFixture]
    public class CoinPurseTests
    {
        [Test]
        public void CoinPursesCanStoreAllTheCoins()
        {
            var purse = new CoinPurse();
            purse.AddPlatinum(3);
            purse.AddGold(53);
            purse.AddSilver(85);
            purse.AddCopper(23);
            Assert.AreEqual(9173, purse.Value);
        }

        [Test]
        public void SetValueWillFillInWithGoldCoinsToMeetValue()
        {
            var purse = new CoinPurse();
            purse.SetValue(37243);
            Assert.AreEqual(372, purse.Gold.Pieces);
            Assert.AreEqual(4, purse.Silver.Pieces);
            Assert.AreEqual(3, purse.Copper.Pieces);
        }
    }
}