// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Treasure
{ 
    public class GoldPieces : CoinsBase
    {
        public const int VALUE = 100; 
        public GoldPieces() : base(VALUE)
        {

        }

        public GoldPieces(int pieces) : base(pieces, VALUE)
        {

        }
    }
}