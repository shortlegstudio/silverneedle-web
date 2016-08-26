using NUnit.Framework;
using SilverNeedle.Dice;

namespace Dice
{
    
    [TestFixture]
    public class DiceStringTests
    {
        [Test]
        public void ParseSingleDiceStrings()
        {
            var d = DiceStrings.ParseSides("d6");
            Assert.AreEqual(DiceSides.d6, d);

            d = DiceStrings.ParseSides("d8");
            Assert.AreEqual(DiceSides.d8, d);
        }

        [Test]
        public void Parse4d6IntoACup()
        {
            var cup = DiceStrings.ParseDice("4d6");
            Assert.AreEqual(4, cup.Dice.Count);
            Assert.AreEqual(DiceSides.d6, cup.Dice[0].Sides);
            Assert.AreEqual(0, cup.Modifier);
        }

        [Test]
        public void Parsed8IntoCup()
        {
            var cup = DiceStrings.ParseDice("d8");
            Assert.AreEqual(1, cup.Dice.Count);
            Assert.AreEqual(DiceSides.d8, cup.Dice[0].Sides);
            Assert.AreEqual(0, cup.Modifier);
        }

        [Test]
        public void ParseD10Plus25IntoCup()
        {
            var cup = DiceStrings.ParseDice("d10+25");
            Assert.AreEqual(1, cup.Dice.Count);
            Assert.AreEqual(DiceSides.d10, cup.Dice[0].Sides);
            Assert.AreEqual(25, cup.Modifier);
        }

        [Test]
        public void Parse4d6Plus12IntoCup()
        {
            var cup = DiceStrings.ParseDice("4d6+12");
            Assert.AreEqual(4, cup.Dice.Count);
            Assert.AreEqual(DiceSides.d6, cup.Dice[0].Sides);
            Assert.AreEqual(12, cup.Modifier);
        }

    }
}