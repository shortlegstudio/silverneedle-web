// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Treasure
{ 
    public class PlatinumPieces : CoinsBase
    {
        private const int PLATINUM_VALUE = 1000; 
        public PlatinumPieces() : base(PLATINUM_VALUE)
        {

        }

        public PlatinumPieces(int pieces) : base(pieces, PLATINUM_VALUE)
        {

        }
    }
}