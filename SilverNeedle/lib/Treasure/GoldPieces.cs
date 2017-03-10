// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Treasure
{ 
    public class GoldPieces : CoinsBase
    {
        private const int GOLD_COIN_VALUE = 100; 
        public GoldPieces() : base(GOLD_COIN_VALUE)
        {

        }

        public GoldPieces(int pieces) : base(pieces, GOLD_COIN_VALUE)
        {

        }
    }
}