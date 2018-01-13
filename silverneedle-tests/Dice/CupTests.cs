// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Dice
{
    using System.Linq;
    using Xunit;
    using SilverNeedle.Dice;
    using SilverNeedle.Serialization;
    
    public class CupTests
    {

        [Fact]
        public void AnyTypeOfDieMayBeAddedToTheCup()
        {
            var cup = new Cup();
            cup.AddDie(Die.D4());
            cup.AddDie(Die.D10());
            cup.AddDie(Die.D6());

            Assert.Equal(new Die[] { Die.D4(), Die.D10(), Die.D6() }, cup.Dice);
        }

        [Fact]
        public void ItRollsAllTheDiceWhenRollingTheCup()
        {
            var cup = new Cup();
            cup.AddDie(Die.D6());
            cup.AddDie(Die.D6());
            cup.Roll();
            Assert.True(cup.Dice.All(x => x.LastRoll > 0));
        }

        [Fact]
        public void ResultIsTheSumOfAllDiceRolled()
        {
            var cup = new Cup();
            cup.AddDie(Die.D6());
            cup.AddDie(Die.D6());
            var result = cup.Roll();
            Assert.Equal(result, cup.Dice.Sum(x => x.LastRoll));
        }

        [Fact]
        public void MultiplesOfTheSameDiceCanBeAdded()
        {
            var cup = new Cup();
            cup.AddDice(
                Die.GetDice(DiceSides.d6, 4)
            );
            Assert.Equal(new Die[] { Die.D6(), Die.D6(), Die.D6(), Die.D6() }, cup.Dice);
        }

        [Fact]
        public void CupCanBeCreatedWithArrayOfDice()
        {
            var cup = new Cup(Die.GetDice(DiceSides.d6, 4));
            Assert.Equal(new Die[] { Die.D6(), Die.D6(), Die.D6(), Die.D6() }, cup.Dice);
        }

        [Fact]
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

            Assert.Equal(manualSum - lowest, sumTop3);
        }

        [Fact]
        public void CupCanHaveABaseValueForTheRoll()
        {
            var cup = new Cup();
            cup.AddDie(Die.D4());
            cup.Modifier = 20;
            Assert.True(cup.Roll() >= 20);
        }

        [Fact]
        public void FormatsCupIntoADiceString()
        {
            var cup = new Cup();
            cup.AddDie(Die.D10());
            Assert.Equal("1d10", cup.ToString());
            cup.AddDie(Die.D10());
            Assert.Equal("2d10", cup.ToString());
            cup.Modifier = 5;
            Assert.Equal("2d10+5", cup.ToString());
            cup.AddDie(Die.D6());
            Assert.Equal("2d10+1d6+5", cup.ToString());

        }

        [Fact]
        public void CanBeSetToAllowsRollTheMaximumValue()
        {
            var cup = new Cup();
            cup.AddDice(Die.GetDice(DiceSides.d6, 10));
            cup.MaximizeAmount = true;
            Assert.Equal(60, cup.Roll());
            Assert.Equal(60, cup.Roll());
            Assert.Equal(60, cup.Roll());
        }

        [Fact]
        public void LoadFromYaml()
        {
            var yaml = @"---
name: Cup Name
dice: 1d6+1";

            var cup = new Cup(yaml.ParseYaml());
            Assert.Equal("Cup Name", cup.Name);
            Assert.Equal(DiceSides.d6, cup.Dice.First().Sides);
            Assert.Equal(1, cup.Modifier);

        }
    }
}