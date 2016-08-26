using System.Linq;
using NUnit.Framework;
using SilverNeedle.Dice;

namespace Dice
{
    
    [TestFixture]
    public class CupTests
    {

        [Test]
        public void AnyTypeOfDieMayBeAddedToTheCup()
        {
            var cup = new Cup();
            cup.AddDie(Die.D4());
            cup.AddDie(Die.D10());
            cup.AddDie(Die.D6());

            Assert.AreEqual(new Die[] { Die.D4(), Die.D10(), Die.D6() }, cup.Dice);
        }

        [Test]
        public void ItRollsAllTheDiceWhenRollingTheCup()
        {
            var cup = new Cup();
            cup.AddDie(Die.D6());
            cup.AddDie(Die.D6());
            cup.Roll();
            Assert.IsTrue(cup.Dice.All(x => x.LastRoll > 0));
        }

        [Test]
        public void ResultIsTheSumOfAllDiceRolled()
        {
            var cup = new Cup();
            cup.AddDie(Die.D6());
            cup.AddDie(Die.D6());
            var result = cup.Roll();
            Assert.AreEqual(result, cup.Dice.Sum(x => x.LastRoll));
        }

        [Test]
        public void MultiplesOfTheSameDiceCanBeAdded()
        {
            var cup = new Cup();
            cup.AddDice(
                Die.GetDice(DiceSides.d6, 4)
            );
            Assert.AreEqual(new Die[] { Die.D6(), Die.D6(), Die.D6(), Die.D6() }, cup.Dice);
        }

        [Test]
        public void CupCanBeCreatedWithArrayOfDice()
        {
            var cup = new Cup(Die.GetDice(DiceSides.d6, 4));
            Assert.AreEqual(new Die[] { Die.D6(), Die.D6(), Die.D6(), Die.D6() }, cup.Dice);
        }

        [Test]
        public void ResultsCanBeFilteredByTakingTheHighestNumberOfDice()
        {
            //For stats we frequently roll 4d6 and want the top 3...
            var cup = new Cup(Die.GetDice(DiceSides.d6, 4));
            cup.Roll();
            var sumTop3 = cup.SumTop(3);

            var manualSum = 0;
            var lowest = 100;
            foreach (var d in cup.Dice)
            {
                manualSum += d.LastRoll;
                if (d.LastRoll < lowest)
                    lowest = d.LastRoll;
            }

            Assert.AreEqual(manualSum - lowest, sumTop3);
        }

        [Test]
        public void CupCanHaveABaseValueForTheRoll()
        {
            var cup = new Cup();
            cup.AddDie(Die.D4());
            cup.Modifier = 20;
            Assert.GreaterOrEqual(cup.Roll(), 20);
        }

        [Test]
        public void FormatsCupIntoADiceString()
        {
            var cup = new Cup();
            cup.AddDie(Die.D10());
            Assert.AreEqual("1d10", cup.ToString());
            cup.AddDie(Die.D10());
            Assert.AreEqual("2d10", cup.ToString());
            cup.Modifier = 5;
            Assert.AreEqual("2d10+5", cup.ToString());
            cup.AddDie(Die.D6());
            Assert.AreEqual("2d10+1d6+5", cup.ToString());

        }
    }
}