// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Treasure
{ 
    public class SilverPieces : CoinsBase
    {
        private const int SILVER_VALUE = 10; 
        public SilverPieces() : base(SILVER_VALUE)
        {

        }

        public SilverPieces(int pieces) : base(pieces, SILVER_VALUE)
        {

        }
    }
}