// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

using Xunit;
using SilverNeedle.Dice;

namespace Dice
{
    
    public class DiceStringTests
    {
        [Fact]
        public void ParseSingleDiceStrings()
        {
            var d = DiceStrings.ParseSides("d6");
            Assert.Equal(DiceSides.d6, d);

            d = DiceStrings.ParseSides("d8");
            Assert.Equal(DiceSides.d8, d);
        }

        [Fact]
        public void Parse4d6IntoACup()
        {
            var cup = DiceStrings.ParseDice("4d6");
            Assert.Equal(4, cup.Dice.Count);
            Assert.Equal(DiceSides.d6, cup.Dice[0].Sides);
            Assert.Equal(0, cup.Modifier);
        }

        [Fact]
        public void Parsed8IntoCup()
        {
            var cup = DiceStrings.ParseDice("d8");
            Assert.Equal(1, cup.Dice.Count);
            Assert.Equal(DiceSides.d8, cup.Dice[0].Sides);
            Assert.Equal(0, cup.Modifier);
        }

        [Fact]
        public void ParseD10Plus25IntoCup()
        {
            var cup = DiceStrings.ParseDice("d10+25");
            Assert.Equal(1, cup.Dice.Count);
            Assert.Equal(DiceSides.d10, cup.Dice[0].Sides);
            Assert.Equal(25, cup.Modifier);
        }

        [Fact]
        public void Parse4d6Plus12IntoCup()
        {
            var cup = DiceStrings.ParseDice("4d6+12");
            Assert.Equal(4, cup.Dice.Count);
            Assert.Equal(DiceSides.d6, cup.Dice[0].Sides);
            Assert.Equal(12, cup.Modifier);
        }

        [Fact]
        public void EmptyStringIsAnEmpyCup()
        {
            var cup = DiceStrings.ParseDice("");
            Assert.Equal(0, cup.Count);
            Assert.Equal("", cup.ToString());
        }
    }
}