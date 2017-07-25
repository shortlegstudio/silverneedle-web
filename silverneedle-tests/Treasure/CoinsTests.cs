// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Treasure
{
    using Xunit;
    using SilverNeedle.Treasure;

    public class CoinsTests
    {
        [Fact]
        public void GoldIsHundredCopper()
        {
            var goldCoins = new GoldPieces(30);
            Assert.Equal(3000, goldCoins.Value);
        }

        [Fact]
        public void SilverIsTenCopper()
        {
            var silver = new SilverPieces(24);
            Assert.Equal(240, silver.Value);
        }

        [Fact]
        public void CopperIsTheBaseOfValues()
        {
            var copper = new CopperPieces(382);
            Assert.Equal(382f, copper.Value);
        }

        [Fact]
        public void PlatinumIsThousandPieces()
        {
            var platinum = new PlatinumPieces(5);
            Assert.Equal(5000, platinum.Value);
        }

        [Fact]
        public void CanConvertACoinStringIntoGoldCoins()
        {
            var gp = "20gp";
            var coins = gp.ToCoins();
            Assert.IsType(typeof(GoldPieces), coins);
            Assert.Equal(20, coins.Pieces);
        }

        [Fact]
        public void CanConvertACoinStringIntoPlatinumCoins()
        {
            var pp = "12pp";
            var coins = pp.ToCoins();
            Assert.IsType(typeof(PlatinumPieces), coins);
            Assert.Equal(12, coins.Pieces);
        }

        [Fact]
        public void CanConvertACoinStringIntoSilverCoins()
        {
            var sp = "39sp";
            var coins = sp.ToCoins();
            Assert.IsType(typeof(SilverPieces), coins);
            Assert.Equal(39, coins.Pieces);
        }

        [Fact]
        public void CanConvertACoinStringIntoCopperCoins()
        {
            var cp = "2cp";
            var coins = cp.ToCoins();
            Assert.IsType(typeof(CopperPieces), coins);
            Assert.Equal(2, coins.Pieces);
        }
    }
}