// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Treasure
{
    using System;
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

        [Test]
        public void SpendingMoneySubtractsFromThePurse()
        {
            var purse = new CoinPurse();
            purse.SetValue(58342);
            purse.Spend(7328);
            Assert.AreEqual(51014, purse.Value);            
        }

        [Test]
        public void CanInitializePurseToValue()
        {
            var purse = new CoinPurse(4824);
            Assert.AreEqual(4824, purse.Value);
        }

        [Test]
        public void IfYouDoNotHaveEnoughMoneyThrowError()
        {
            var purse = new CoinPurse(3923);
            Assert.Throws<InsufficientFundsException>(() => purse.Spend(484234));
        }

        [Test]
        public void CanCheckIfYouCanAffordAnItem()
        {
            var purse = new CoinPurse(4842);
            var item = new DummyItem();
            item.Value = 3293;
            var expensive = new DummyItem();
            expensive.Value = 49348;

            Assert.That(purse.CanAfford(item), Is.True);
            Assert.That(purse.CanAfford(expensive), Is.False);
        }

        private class DummyItem : SilverNeedle.Equipment.IGear
        {
            public string Name { get; set; }

            public float Weight { get; set; }

            public int Value { get; set; }
        }
    }
}