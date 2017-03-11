// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Treasure
{
    using NUnit.Framework;
    using SilverNeedle.Treasure;

    [TestFixture]
    public class CoinsTests
    {
        [Test]
        public void GoldIsHundredCopper()
        {
            var goldCoins = new GoldPieces(30);
            Assert.AreEqual(3000, goldCoins.Value);
        }

        [Test]
        public void SilverIsTenCopper()
        {
            var silver = new SilverPieces(24);
            Assert.AreEqual(240, silver.Value);
        }

        [Test]
        public void CopperIsTheBaseOfValues()
        {
            var copper = new CopperPieces(382);
            Assert.AreEqual(382f, copper.Value);
        }

        [Test]
        public void PlatinumIsThousandPieces()
        {
            var platinum = new PlatinumPieces(5);
            Assert.AreEqual(5000, platinum.Value);
        }

        [Test]
        public void CanConvertACoinStringIntoGoldCoins()
        {
            var gp = "20gp";
            var coins = gp.ToCoins();
            Assert.IsInstanceOf(typeof(GoldPieces), coins);
            Assert.AreEqual(20, coins.Pieces);
        }

        [Test]
        public void CanConvertACoinStringIntoPlatinumCoins()
        {
            var pp = "12pp";
            var coins = pp.ToCoins();
            Assert.IsInstanceOf(typeof(PlatinumPieces), coins);
            Assert.AreEqual(12, coins.Pieces);
        }

        [Test]
        public void CanConvertACoinStringIntoSilverCoins()
        {
            var sp = "39sp";
            var coins = sp.ToCoins();
            Assert.IsInstanceOf(typeof(SilverPieces), coins);
            Assert.AreEqual(39, coins.Pieces);
        }

        [Test]
        public void CanConvertACoinStringIntoCopperCoins()
        {
            var cp = "2cp";
            var coins = cp.ToCoins();
            Assert.IsInstanceOf(typeof(CopperPieces), coins);
            Assert.AreEqual(2, coins.Pieces);
        }

    }
}