// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Dice
{
    using Xunit;
    using SilverNeedle.Dice;
    using System.Linq;
    
    public class DiceTests
    {
        private void ValidateAllSides(Die die)
        {
            var results = new bool[die.SideCount];
            for (var counter = 0; counter < 1000 * die.SideCount; counter++)
            {
                var roll = die.Roll();
                results[roll - 1] = true;
                if (results.All(x => x))
                {
                    return;
                }
            }
            Assert.True(false, "Not all sides were returned: " + string.Join(",", results.Select(z => z.ToString())));
        }

        [Fact]
        public void D4ReturnsAllSideValues()
        {
            ValidateAllSides(Die.D4());
            Assert.True(true);
        }

        [Fact]
        public void D6ReturnsAllSideValues()
        {
            ValidateAllSides(Die.D6());
            Assert.True(true);
        }

        [Fact]
        public void D8ReturnsAllSideValues()
        {
            ValidateAllSides(Die.D8());
            Assert.True(true);
        }

        [Fact]
        public void D10ReturnsAllSideValues()
        {
            ValidateAllSides(Die.D10());
            Assert.True(true);
        }

        [Fact]
        public void D12ReturnsAllSideValues()
        {
            ValidateAllSides(Die.D12());
            Assert.True(true);
        }

        [Fact]
        public void D20ReturnsAllSideValues()
        {
            ValidateAllSides(Die.D20());
            Assert.True(true);
        }

        [Fact]
        public void D100ReturnsAllSideValues()
        {
            ValidateAllSides(Die.D100());
            Assert.True(true);
        }

        [Fact]
        public void GetProperSidesOfDefaultDie()
        {
            Assert.Equal(DiceSides.d4, Die.D4().Sides);
            Assert.Equal(DiceSides.d6, Die.D6().Sides);
            Assert.Equal(DiceSides.d8, Die.D8().Sides);
            Assert.Equal(DiceSides.d10, Die.D10().Sides);
            Assert.Equal(DiceSides.d12, Die.D12().Sides);
            Assert.Equal(DiceSides.d20, Die.D20().Sides);
            Assert.Equal(DiceSides.d100, Die.D100().Sides);
        }

        [Fact]
        public void ToStringReturnsALogicalVersionOfDie()
        {
            var d4 = Die.D4();
            var result = d4.Roll();
            var expectedString = string.Format("[Die: Sides={0}, LastRoll={1}]", d4.Sides, result);
            Assert.Equal(expectedString, d4.ToString());
        }

        [Fact]
        public void TwoDifferentDiceWithTheSameSidesAndRollAreEqual()
        {
            var dieOne = Die.D8();
            var dieTwo = Die.D8();

            Assert.Equal(dieOne, dieTwo);
        }

        [Fact]
        public void CreateAnArrayOfDiceByPassingANumberIntoTheHelperMethod()
        {
            var diceArray = Die.GetDice(DiceSides.d12, 4);
            Assert.Equal(new Die[] { Die.D12(), Die.D12(), Die.D12(), Die.D12() }, diceArray);
        }
    }
}