// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Treasure
{
    using System;
    using Xunit;
    using SilverNeedle.Treasure;

    public class CoinPurseTests
    {
        [Fact]
        public void CoinPursesCanStoreAllTheCoins()
        {
            var purse = new CoinPurse();
            purse.AddPlatinum(3);
            purse.AddGold(53);
            purse.AddSilver(85);
            purse.AddCopper(23);
            Assert.Equal(9173, purse.Value);
        }

        [Fact]
        public void SetValueWillFillInWithGoldCoinsToMeetValue()
        {
            var purse = new CoinPurse();
            purse.SetValue(37243);
            Assert.Equal(372, purse.Gold.Pieces);
            Assert.Equal(4, purse.Silver.Pieces);
            Assert.Equal(3, purse.Copper.Pieces);
        }

        [Fact]
        public void SpendingMoneySubtractsFromThePurse()
        {
            var purse = new CoinPurse();
            purse.SetValue(58342);
            purse.Spend(7328);
            Assert.Equal(51014, purse.Value);            
        }

        [Fact]
        public void CanInitializePurseToValue()
        {
            var purse = new CoinPurse(4824);
            Assert.Equal(4824, purse.Value);
        }

        [Fact]
        public void IfYouDoNotHaveEnoughMoneyThrowError()
        {
            var purse = new CoinPurse(3923);
            Assert.Throws<InsufficientFundsException>(() => purse.Spend(484234));
        }

        [Fact]
        public void CanCheckIfYouCanAffordAnItem()
        {
            var purse = new CoinPurse(4842);
            var item = new DummyItem();
            item.Value = 3293;
            var expensive = new DummyItem();
            expensive.Value = 49348;

            Assert.True(purse.CanAfford(item));
            Assert.False(purse.CanAfford(expensive));
        }

        private class DummyItem : SilverNeedle.Equipment.IGear
        {
            public string Name { get; set; }

            public float Weight { get; set; }

            public int Value { get; set; }

            public bool GroupSimilar { get; set; }
        }
    }
}