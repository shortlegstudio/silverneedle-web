// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Treasure
{
    public class CoinsBase
    {
        public int Pieces { get; set; }
        public int CoinValue { get; private set; }
        public int Value { get { return Pieces * CoinValue; } }

        public string Abbreviation { get; private set; }
        protected CoinsBase(int coinValue, string abbr)
        {
            CoinValue = coinValue;
            Abbreviation = abbr;
        }

        protected CoinsBase(int pieces, int coinValue, string abbr): this(coinValue, abbr)
        {
            Pieces = pieces;
        }
    }
}