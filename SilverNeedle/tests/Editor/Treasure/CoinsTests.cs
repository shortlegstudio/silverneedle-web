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
    }
}